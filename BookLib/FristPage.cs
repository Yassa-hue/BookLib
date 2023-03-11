namespace BookLib;

public static class FirstPage
{
    

    static void FirstPageWelcomeMsg(Context context)
    {
        Console.WriteLine("Welcome to our Online Reader");
    }

    public static Action<Context> GetFirstPage(Context context)
    {
        return context => FirstPageWelcomeMsg(context);
    }


    public static List<(string pageName, Action<Context> funcDel)> GetFirstPageNextChoices(Context context)
    {
        return new List<(string pageName, Action<Context> funcDel)>
        {
            ("Log In page", LogIn.GetLogInPage()),
            ("Sign Up page", SignUp.GetSignUpPage())
        };
    }



}