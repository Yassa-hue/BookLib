namespace BookLib;

public class Page
{
    public static void OpenPage(Context context, Action<Context> pageLogic)
    {
        pageLogic(context);
        
        var nextSteps = getNextSteps(context);

        var (nextPageName, nextPageFunc) = NextStep.getNextStep(nextSteps);

        context.pageName = nextPageName;
        
        OpenPage(context, nextPageFunc);        
        
    }

    public static List<(string pageName, Action<Context> funcDel)> getNextSteps(Context context)
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
            
            
        }

        return nextSteps;
    }
    

}


