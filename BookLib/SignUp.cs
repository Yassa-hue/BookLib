using Microsoft.Data.Sqlite;
namespace BookLib;

public class SignUp
{
    static UserSignUpData TakeUserSignUpData()
    {
        // to be implemented

        return new UserSignUpData("", "", "", "", "");
    }

    static bool ValidUserData(UserSignUpData userSignUpData)
    {
        // to be implemented
        return true;
    }
    
    static UserSignUpData ValidateUserData(UserSignUpData userSignUpData)
    {
        if (!ValidUserData(userSignUpData))
        {
            throw new Exception("Invalid user data");
        }

        return userSignUpData;
    }

    static User StoreUserInDb(UserSignUpData userSignUpData, SqliteConnection dbConn)
    {
        // to be implemented
        return new User(-1, "", "", "", false);
    }
    

    static Func<User> GetSignUpFunc(SqliteConnection dbConn)
    {
        var takeUserDataDel = TakeUserSignUpData;
        var validateUserDataDel = ValidateUserData;
        var storeUserDataDel = StoreUserInDb;
        
        var signUp = takeUserDataDel
            .Compose(validateUserDataDel)
            .Compose(storeUserDataDel.ComposeWithDbConn(dbConn));
        
        return signUp;
    }


    static void SignUpPage(Context context)
    {
        var signUp = GetSignUpFunc(context.dbConnection);
        
        Console.WriteLine("Sign in page \n\n\n");

        User user = signUp();

        context.user = user;
    }


    public static Action<Context> GetSignUpPage() 
    {
        return context => SignUpPage(context);
    }


    public static List<NextPage> GetSignUpNextChoices()
    {
        return new List<NextPage>
        {
            new NextPage{pageName = "Home page", pageLogic = HomePage.GetHomePage(), adminOnly = false}
        };
    }
}

public record UserSignUpData(string name, string email, string password, string repeatedPassword, string phoneNum);
