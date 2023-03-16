namespace BookLib;


using GetNextStepDel = Func<Context, PageNameAndLogic, PageNameAndLogic?>;
public class Page
{
    private static void OpenPage(Context context, Action<Context> pageLogic, GetNextStepDel getNextStep)
    {
        // empty last page
        var lastPage = new PageNameAndLogic("", c => {});
        
        while (true)
        {
            
            // perform the page logic
            pageLogic(context);
            
            var possibleNextStep = getNextStep(context, lastPage);

            
            if (possibleNextStep != null)
            {
                // store last page
                lastPage = new PageNameAndLogic(context.pageName, pageLogic);
                
                // move to the next page
                context.pageName = possibleNextStep.pageName;
                pageLogic = possibleNextStep.funcDel;
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