namespace BookLib;

public class Page
{
    public static void OpenPage(Context context, Action<Context> pageLogic, 
        Func<Context, (string pageName, Action<Context> funcDel)> getNextStep)
    {
        pageLogic(context);
        
        var (nextPageName, nextPageFunc) = getNextStep(context);

        context.pageName = nextPageName;
        
        OpenPage(context, nextPageFunc, getNextStep);        
        
    }

    public static Action<Action<Context>> CloseOpenPageFunOnContextAndNextPageFunc(Context context,
        Func<Context, (string pageName, Action<Context> funcDel)> getNextStep)
    {
        return pageLogic => OpenPage(context, pageLogic, getNextStep);
    }


}