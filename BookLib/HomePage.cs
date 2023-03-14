namespace BookLib;

public class HomePage
{

    public const string PageName = "Home page";
    
    // home page next Options
    public static List<NextPage> GetNextsteps()
    {
        var nextSteps = new List<NextPage>
        {
            new NextPage{pageName = "Info page", pageLogic = InfoPage.getInfoPage(), adminOnly = false },
            new NextPage{ pageName = "List books", pageLogic = ListBooksPage.GetListBooks(), adminOnly = false}
        };

        return nextSteps;
    }

    static void HomePageLogic(Context context)
    {
        Console.WriteLine("Welcome " + context.user.name);
    }


    public static Action<Context> GetHomePage()
    {
        return context => HomePageLogic(context);
    }


}