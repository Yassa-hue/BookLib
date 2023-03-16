namespace BookLib;

public class HomePage
{

    public const string PageName = "Home page";

    static void HomePageLogic(Context context)
    {
        Console.WriteLine("Welcome " + context.user.name);
    }


    public static Action<Context> GetHomePageLogic()
    {
        return context => HomePageLogic(context);
    }

    
    // home page next Options
    public static List<NextPage> GetNextNextChoices()
    {
        var nextSteps = new List<NextPage>
        {
            InfoPage.GetInfoPage(),
            ListBooksPage.GetListBooksPage(),
            AddBookPage.GetAddBookPage(),
            ListPreferredBooksPage.GetListPreferredBooksPage()
        };

        return nextSteps;
    }
    
    public static NextPage GetHomePage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetHomePageLogic(), adminOnly = false };
    }


}