

namespace BookLib;


public static class NextStep
{

    public static (string pageName, Action<Context> funcDel) getNextStep(List<(string pageName, Action<Context> funcDel)> nextStepChoices)
    {
        // to be implemented


        return nextStepChoices[0];
    }

}


