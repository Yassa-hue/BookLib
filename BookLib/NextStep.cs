

namespace BookLib;


public static class NextStep
{

    
    public static List<(string pageName, Action<Context> funcDel)> GetAllPossibleNextSteps(Context context)
    {
        List<(string pageName, Action<Context> funcDel)> nextSteps = new List<(string pageName, Action<Context> funcDel)>();
        switch (context.pageName)
        {

            case "First Page":
            {
                nextSteps = FirstPage.GetFirstPageNextChoices(context);
                break;
            }
                
            
            case "Log in":
            {
                nextSteps = LogIn.GetLogInNextChoices(context);
                break;
            }

            case "Sign up":
            {
                nextSteps = SignUp.GetSignUpNextChoices(context);
                break;
            }

            case "List books":
            {
                nextSteps = ListBooksPage.GetListBooksNextChoices(context);
                break;
            }
            
            
        }

        return nextSteps;
    }
    
    public static (string pageName, Action<Context> funcDel) SelectNextStep(List<(string pageName, Action<Context> funcDel)> allPossibleNextSteps)
    {
        // to be implemented


        return allPossibleNextSteps[0];
    }


    public static Func<Context, (string pageName, Action<Context> funcDel)> GetNextStepFunc()
    {
        var getAllPossibleNextStepsDel = GetAllPossibleNextSteps;
        var selectNextStepDel = SelectNextStep;

        var getNextStepDel = getAllPossibleNextStepsDel.Compose(selectNextStepDel);

        return getNextStepDel;
    }

}


