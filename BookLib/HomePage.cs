namespace BookLib;

public class HomePage
{
    // home page next Options
    public static List<(string pageName, Action<Context> funcDel)> getNextsteps(bool isAdmin)
    {
        var nextSteps = new List<(string pageName, Action<Context> funcDel, bool adminOnly)>
        {
            ("Info page", InfoPage.getInfoPage(), false)
        };

        return nextSteps
                .Where(nextStep => (!nextStep.adminOnly || nextStep.adminOnly == isAdmin) )
                .Select(nextStep => (nextStep.pageName, nextStep.funcDel))
                .ToList();
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