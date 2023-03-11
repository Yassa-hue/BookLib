

namespace BookLib;


public static class NextStep
{

    public static Action<Context> getNextStep(List<(string pageName, Action<Context> funcDel)> nextStepChoices)
    {
        // to be implemented
        return nextStepChoices[0].funcDel;
    }

}


