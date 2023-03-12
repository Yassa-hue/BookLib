

namespace BookLib;


public static class NextStep
{

    
    public static List<(string pageName, Action<Context> funcDel)> GetAllPossibleNextSteps(Context context, 
        Dictionary<string, List<(string pageName, Action<Context> funcDel)>> pageRouter)
    {
        // to be implemented
        
        return new List<(string pageName, Action<Context> funcDel)>();
    }
    
    public static (string pageName, Action<Context> funcDel) SelectNextStep(List<(string pageName, Action<Context> funcDel)> allPossibleNextSteps)
    {
        // to be implemented


        return allPossibleNextSteps[0];
    }


    public static Func<Context, List<(string pageName, Action<Context> funcDel)>> EncloseOverPageRouter(
        Dictionary<string, List<(string pageName, Action<Context> funcDel)>> pageRouter,
        Func<Context,
            Dictionary<string, List<(string pageName, Action<Context> funcDel)>>,
            List<(string pageName, Action<Context> funcDel)>> GetAllNextStepsDel)
    {
        return c => GetAllNextStepsDel(c, pageRouter);
    }


    public static Func<Context, (string pageName, Action<Context> funcDel)> GetNextStepFunc(Dictionary<string, List<(string pageName, Action<Context> funcDel)>> pageRouter)
    {
        var getAllPossibleNextStepsDel = EncloseOverPageRouter(pageRouter, GetAllPossibleNextSteps);
        var selectNextStepDel = SelectNextStep;

        var getNextStepDel = getAllPossibleNextStepsDel.Compose(selectNextStepDel);

        return getNextStepDel;
    }

}


