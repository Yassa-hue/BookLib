using Microsoft.Data.Sqlite;

namespace BookLib;

public static class LogIn
{

    // Log In functions
    static UserCred TakeUserCred()
    {
        Console.WriteLine("Input your email and password");

        string email = Console.ReadLine();
            
        string password = Console.ReadLine();

        var userCred = new UserCred(email, password);

        return userCred;
    }

    static bool validUserCred(UserCred userCred)
    {
        // to be implemented
        return true;
    }
    
    static UserCred ValidateUserCred(UserCred userCred)
    {
        if (!validUserCred(userCred))
        {
            throw new Exception("Invalid user credential");
        }

        return userCred;
    }

    static User getUserData(UserCred userCred, SqliteConnection dbConn)
    {
        // to be implemented
        return new User(-1, "", "", "", false);
    }
    

    static Func<User> GetLogInFunc(SqliteConnection dbConn)
    {
        var takeUserCredDel = TakeUserCred;
        var validateUserCredDel = ValidateUserCred;
        var getUserDataDel = getUserData;
        
        var logIn = takeUserCredDel
                        .Compose(validateUserCredDel)
                        .Compose(getUserDataDel.ComposeWithDbConn(dbConn));
        
        return logIn;
    }


    static void LogInPage(Context context)
    {
        var logIn = GetLogInFunc(context.dbConnection);
        
        Console.WriteLine("Log in page \n\n\n");

        User user = logIn();

        context.user = user;
    }


    public static Action<Context> GetLogInPage(Context context) 
    {
        return context => LogInPage(context);
    }


    public static Func<Context, List<(string pageName, Action<Context> funcDel)>> GetLogInNextChoices(Context context)
    {
        return context => new List<(string pageName, Action<Context> funcDel)>
        {
            ("Home page", HomePage.GetHomePage(context))
        };
    }




    // Sign Up functions
    static bool validUserSignUpData(UserSignUpData userSignUpData)
    {
        // to be implemented
        return true;
    }
    
    static UserSignUpData validateUserSignUpData(UserSignUpData userSignUpData)
    {
        if (!validUserSignUpData(userSignUpData))
        {
            throw new Exception("Invalid user user data");
        }

        return userSignUpData;
    }

    static User setUserData(UserSignUpData userSignUpData, SqliteConnection dbConn)
    {
        // to be implemented
        return new User(-1, "", "", "", false);
    }

    
    public static Func<UserSignUpData, User> GetSignUpFunc(SqliteConnection dbConn)
    {
        var validateUserSignUpDataDel = validateUserSignUpData;
        var setUserDataDel = setUserData;
        var readySetUserData = setUserDataDel.ComposeWithDbConn(dbConn);
                
            
        return validateUserSignUpDataDel.Compose(readySetUserData);
    }
}



public record UserCred(string email, string password);
public record UserSignUpData(string name, string email, string password, string repeatedPassword, string phoneNum);
public record User(int id, string name, string email, string phoneNum, bool isAdmin);
