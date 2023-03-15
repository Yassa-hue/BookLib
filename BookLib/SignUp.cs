using Microsoft.Data.Sqlite;
using System.Net.Mail;
namespace BookLib;

public class SignUp
{

    public const string PageName = "Sign Up page";
    static UserSignUpData TakeUserSignUpData()
    {
        Console.WriteLine("Input your data : your name, email, password and repeat password (each on separate line)");

        string name = Console.ReadLine();
        string email = Console.ReadLine();
        string password = Console.ReadLine();
        string repeatedPassword = Console.ReadLine();

        return new UserSignUpData(name,
                                    email, 
                                    password, 
                                    repeatedPassword);
    }

    static bool ValidUserData(UserSignUpData userSignUpData)
    {
        // validate name
        if (userSignUpData.name.Length < 8)
        {
            return false;
        }
            
        // validate email
        if (!MailAddress.TryCreate(userSignUpData.email, out var mailAddress))
        {
            return false;
        }

        // validate password
        return (userSignUpData.password.Length >= 8
                 || userSignUpData.password == userSignUpData.repeatedPassword);
        
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
        var dbCommand = dbConn.CreateCommand();

        // store user in the db
        dbCommand.CommandText = $"INSERT INTO User(name, email, password, is_admin) VALUES('{userSignUpData.name}', '{userSignUpData.email}', '{userSignUpData.password}', 0);";
        dbCommand.ExecuteNonQuery();

        // get the id of the user
        dbCommand.CommandText = $"SELECT id FROM User WHERE name == '{userSignUpData.name}';";
        var dbReader = dbCommand.ExecuteReader();
        dbReader.Read();
        
        return new User(int.Parse(dbReader.GetString(0)),
                    userSignUpData.name,
                    userSignUpData.email,
                    false);
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

        const int maxNumOfTrys = 3;

        int numOfTrys = 0;

        while (numOfTrys < maxNumOfTrys)
        {
            try
            {
                User user = signUp();
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


    public static Action<Context> GetSignUpPageLogic() 
    {
        return context => SignUpPage(context);
    }


    public static List<NextPage> GetSignUpNextChoices()
    {
        return new List<NextPage>
        {
            HomePage.GetHomePage()
        };
    }

    public static NextPage GetSignUpPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetSignUpPageLogic(), adminOnly = false };
    }
    
    
}

public record UserSignUpData(string name, string email, string password, string repeatedPassword);
