namespace BookLib;

public static class FirstPage
{


    public static void openFristPage(Context context, Func<List<(string pageName, Action<Context> funcDel)>> getNextSteps)
    {
        Console.WriteLine("Welcome Page");

        var nextSteps = getNextSteps();
        
        var nextStep = NextStep.getNextStep(nextSteps);

        nextStep(context);
    }



}