namespace BookLib;

public static class FirstPage
{


    public const string PageName = "First page";
    

    static void FirstPageWelcomeMsg(Context context)
    {
        Console.WriteLine("Welcome to our Online Reader");
    }

    public static Action<Context> GetFirstPage(Context context)
    {
        return context => FirstPageWelcomeMsg(context);
    }


    public static List<NextPage> GetFirstPageNextChoices()
    {
        return new List<NextPage>
        {
            new NextPage{pageName = "Log In page", pageLogic = LogIn.GetLogInPage(), adminOnly = false},
            new NextPage{pageName = "Sign Up page", pageLogic = SignUp.GetSignUpPage(), adminOnly = false}
        };
    }



}