namespace BookLib;

public class AuthPages
{
    public static void logInPage(Context context)
    {
        // to be implemented
        
    }

    public static void signUpPage(Context context)
    {
        // to be implemented    
    }
    
    public static List<(string pageName, Action<Context> funcDel)> getNextsteps()
    {
        var nextSteps = new List<(string pageName, Action<Context> funcDel)>
        {
            ("Log in", logInPage),
            ("Sign up", signUpPage),
        };

        return nextSteps;
    }
}