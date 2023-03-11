namespace BookLib;

public static class FirstPage
{


    public static void openFristPage(Context context, Func<List<(string pageName, Action<Context> funcDel)>> getNextSteps)
    {
        Console.WriteLine("Welcome Page");

        var nextSteps = getNextSteps();

        var nextStepChoices = nextSteps.Select((x) => x.pageName).ToList();

        var nextStep = NextStep.getNextStep(nextStepChoices);

        nextSteps[nextStep].funcDel(context);
    }



}