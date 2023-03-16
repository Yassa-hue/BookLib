namespace BookLib;

public class InfoPage
{


    public const string PageName = "Info page";
    public static List<NextPage> GetNextSteps()
    {
        var nextSteps = new List<NextPage>
        {
            ListPreferredBooksPage.GetListPreferredBooksPage()
        };

        return nextSteps;
    }

    public static void PrintUserInfo(Context context)
    {
        Console.WriteLine("Info page");
        
        Console.WriteLine("\tUser Name : " + context.user.name);
        Console.WriteLine("\tUser Email : " + context.user.email);
        Console.WriteLine($"\tUser is {(context.user.isAdmin ? "" : "Not")} admin");
        
    }

    public static Action<Context> GetInfoPageLogic()
    {
        return context => PrintUserInfo(context);
    }
    
    
    
    
    public static NextPage GetInfoPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetInfoPageLogic(), adminOnly = false };
    }
}