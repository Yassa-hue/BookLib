namespace BookLib;


using GetNextStepDel = Func<Context, (string pageName, Action<Context> funcDel), (string pageName, Action<Context> nextPageLogic)?>;
public class Page
{
    private static void OpenPage(Context context, Action<Context> pageLogic, GetNextStepDel getNextStep)
    {

        while (true)
        {
            
            // perform the page logic
            pageLogic(context);
            
            var possibleNextStep = getNextStep(context, (context.pageName ,pageLogic));

            
            if (possibleNextStep != null)
            {
                context.pageName = possibleNextStep.Value.pageName;
                pageLogic = possibleNextStep.Value.nextPageLogic;
            }
            else // if there is no next page exit the program
            {
                break;
            }
        }     
        
    }

    public static Action<Action<Context>> CloseOnContextAndNextPageFunc(Context context,GetNextStepDel getNextStep)
    {
        return pageLogic => OpenPage(context, pageLogic, getNextStep);
    }


}