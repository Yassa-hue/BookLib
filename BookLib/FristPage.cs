namespace BookLib;

public static class FirstPage
{


    public const string PageName = "First page";
    

    static void FirstPageWelcomeMsg(Context context)
    {
        Console.WriteLine("Welcome to our Online Reader");
    }

    public static Action<Context> GetFirstPageLogic()
    {
        return context => FirstPageWelcomeMsg(context);
    }


    public static List<NextPage> GetFirstPageNextChoices()
    {
        return new List<NextPage>
        {
            LogIn.GetLogInPage(),
            SignUp.GetSignUpPage()
        };
    }


    public static NextPage GetFirstPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetFirstPageLogic(), adminOnly = false };
    }

}