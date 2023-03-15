using System.Linq.Expressions;
using System.Net.Mail;
using Microsoft.Data.Sqlite;

namespace BookLib;

public static class LogIn
{


    public const string PageName = "Log in page";
    
    
    // Log In functions
    static UserCred TakeUserCred()
    {
        Console.WriteLine("Input your email and password");

        string email = Console.ReadLine();
            
        string password = Console.ReadLine();

        var userCred = new UserCred(email, password);

        return userCred;
    }

    static bool ValidUserCred(UserCred userCred)
    {
        if (!MailAddress.TryCreate(userCred.email, out var mailAddress))
        {
            return false;
        }

        return userCred.password.Length >= 8;
    }
    
    static UserCred ValidateUserCred(UserCred userCred)
    {
        if (!ValidUserCred(userCred))
        {
            throw new Exception("Invalid user credential");
        }

        return userCred;
    }

    static User GetUserData(UserCred userCred, SqliteConnection dbConn)
    {
        // to be implemented
        // search about the user in the database
        var dbCommand = dbConn.CreateCommand();

        dbCommand.CommandText = $"SELECT id, name, email, is_admin FROM User WHERE email == '{userCred.email}' AND password == '{userCred.password}';";

        var dbReader = dbCommand.ExecuteReader();

        if (!dbReader.HasRows)
        {
            throw new Exception("No user found");
        }

        dbReader.Read();

        return new User(
                int.Parse(dbReader.GetString(0)),
                dbReader.GetString(1),
                dbReader.GetString(2),
                int.Parse(dbReader.GetString(4)) == 1);
    }
    

    static Func<User> GetLogInFunc(SqliteConnection dbConn)
    {
        var takeUserCredDel = TakeUserCred;
        var validateUserCredDel = ValidateUserCred;
        var getUserDataDel = GetUserData;
        
        var logIn = takeUserCredDel
                        .Compose(validateUserCredDel)
                        .Compose(getUserDataDel.ComposeWithDbConn(dbConn));
        
        return logIn;
    }


    static void LogInPage(Context context)
    {
        var logIn = GetLogInFunc(context.dbConnection);

        const int maxNumOfTrys = 3;

        int numOfTrys = 0;
        
        Console.WriteLine("Log in page \n\n\n");

        while (numOfTrys < maxNumOfTrys)
        {
            try
            {
                User user = logIn();
                context.user = user;
                break;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            numOfTrys += 1;
        }
    }


    public static Action<Context> GetLogInPageLogic() 
    {
        return context => LogInPage(context);
    }


    public static List<NextPage> GetLogInNextChoices()
    {
        return new List<NextPage>
        {
            HomePage.GetHomePage()
        };
    }
    
    
    
    public static NextPage GetLogInPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetLogInPageLogic(), adminOnly = false };
    }
    
}



public record UserCred(string email, string password);
public record User(int id, string name, string email, bool isAdmin);
