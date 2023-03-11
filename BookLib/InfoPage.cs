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

    public static void openInfoPage(Context context,
        Func<bool, List<(string pageName, Action<Context> funcDel)>> getNextSteps)
    {
        Console.WriteLine("Info page");
        
        // to be implemented : info page logic : printing the user info .. ets
        
        
        
        
        
        
        
        var nextSteps = getNextSteps(context.user.isAdmin);

        var nextStep = NextStep.getNextStep(nextSteps);

        nextStep(context);
    }

    public static Action<Context> getInfoPageDriver()
    {
        return (x) => openInfoPage(x, getNextsteps);
    }
}