namespace BookLib;

public class InfoPage
{
    public static List<(string pageName, Action<Context> funcDel)> getNextsteps(bool isAdmin)
    {
        var nextSteps = new List<(string pageName, Action<Context> funcDel, bool adminOnly)>
        {
            // example ("Read book", getReadSessionPageDriver(), false)
        };

        return nextSteps
            .Where(nextStep => nextStep.adminOnly == isAdmin )
            .Select(nextStep => (nextStep.pageName, nextStep.funcDel))
            .ToList();
    }

    public static void openInfoPage(Context context)
    {
        Console.WriteLine("Info page");
        
        // to be implemented : info page logic : printing the user info .. ets
        
        
        
    }

    public static Action<Context> getInfoPage()
    {
        return context => openInfoPage(context);
    }
}