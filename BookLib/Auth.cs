using Microsoft.Data.Sqlite;

namespace BookLib;

public static class Auth
{

    // Log In functions
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
    

    public static Func<UserCred, User> GetLogInFunc(SqliteConnection dbConn)
    {
        var validateUserCredDel = ValidateUserCred;
        var getUserData = Auth.getUserData;
        var readyGetUserData = getUserData.composeWithDbConn(dbConn);
                
            
        return validateUserCredDel.compose(readyGetUserData);
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
        var readySetUserData = setUserDataDel.composeWithDbConn(dbConn);
                
            
        return validateUserSignUpDataDel.compose(readySetUserData);
    }
}



public record UserCred(string email, string password);
public record UserSignUpData(string name, string email, string password, string repeatedPassword, string phoneNum);
public record User(int id, string name, string email, string phoneNum, bool isAdmin);
