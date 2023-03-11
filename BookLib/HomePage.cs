namespace BookLib;

public class HomePage
{
    // home page next Options
    public static List<(string pageName, Action<Context> funcDel)> getNextsteps(bool isAdmin)
    {
        var nextSteps = new List<(string pageName, Action<Context> funcDel, bool adminOnly)>
        {
            // example ("search books", openSearchBooksPage, false)
        };

        return nextSteps
                .Where(nextStep => nextStep.adminOnly == isAdmin )
                .Select(nextStep => (nextStep.pageName, nextStep.funcDel))
                .ToList();
    }

    public static void openHomePage(Context context,
        Func<bool, List<(string pageName, Action<Context> funcDel)>> getNextSteps)
    {
        Console.WriteLine("Home page");

        var nextSteps = getNextSteps(context.user.isAdmin);

        var nextStep = NextStep.getNextStep(nextSteps);

        nextStep(context);
    }

    // open home page 
}