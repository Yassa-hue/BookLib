namespace BookLib;

public class InfoPage
{


    public const string PageName = "Info page";
    public static List<NextPage> getNextsteps(bool isAdmin)
    {
        var nextSteps = new List<NextPage>
        {
            // example ("Read book", getReadSessionPageDriver(), false)
        };

        return nextSteps;
    }

    public static void PrintUserInfo(Context context)
    {
        Console.WriteLine("Info page");
        
        // to be implemented : info page logic : printing the user info .. ets
        
        
        
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