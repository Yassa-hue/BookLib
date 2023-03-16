namespace BookLib;

public class HomePage
{

    public const string PageName = "Home page";
    
    // home page next Options
    public static List<NextPage> GetNextNextChoices()
    {
        var nextSteps = new List<NextPage>
        {
            InfoPage.GetInfoPage(),
            ListBooksPage.GetListBooksPage(),
            AddBookPage.GetAddBookPage()
        };

        return nextSteps;
    }

    static void HomePageLogic(Context context)
    {
        Console.WriteLine("Welcome " + context.user.name);
    }


    public static Action<Context> GetHomePageLogic()
    {
        return context => HomePageLogic(context);
    }
    
    
    public static NextPage GetHomePage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetHomePageLogic(), adminOnly = false };
    }


}