using System.Linq;

namespace BookLib;

using NextPages = List<NextPage>;
using AccessibleNextPages = List<PageNameAndLogic>;
using PageRouter = Dictionary<string, List<NextPage>>;
using GetNextStepFuncTemplate = Func<Context, PageNameAndLogic, PageNameAndLogic?>;
public record PageNameAndLogic(string pageName, Action<Context> funcDel);

public static class NextStep
{

    

    private static AccessibleNextPages GetAllAccessibleNextPages(Context context, PageRouter pageRouter)
    {
        // Filter pages by accessibility
        var accessibleNextPages = pageRouter[context.pageName]
            .Where(page => !page.adminOnly || page.adminOnly == context.user.isAdmin)
            .Select(page => new PageNameAndLogic(page.pageName, page.pageLogic)).ToList();

        return accessibleNextPages;
    }
    
    private static PageNameAndLogic? SelectNextStep(
        AccessibleNextPages allPossibleNextSteps, 
        string currentPageName,
        PageNameAndLogic backStep)
    {
        // take the user choice and turn it into page
        Console.WriteLine("What is your next step ?");

        if (currentPageName != FirstPage.PageName
            && currentPageName != LogIn.PageName
            && currentPageName != SignUp.PageName)
        {
            Console.WriteLine("-3 ) Exit program");
            Console.WriteLine("-2 ) Back step");
            Console.WriteLine("-1 ) Home page");
        }

        var selectChoices = allPossibleNextSteps
            .Select((step, index) => index + " ) " + step.pageName)
            .ToList();

        foreach (var choice in selectChoices)
        {
            Console.WriteLine(choice);
        }

        var userChoice = int.Parse(Console.ReadLine());

        if (userChoice == -3)
        {
            return null;
        }

        if (userChoice == -2)
        {
            return backStep;
        }

        if (userChoice == -1)
        {
            return new PageNameAndLogic(HomePage.PageName, HomePage.GetHomePageLogic());
        }

        return allPossibleNextSteps[userChoice];
    }


    private static Func<Context, AccessibleNextPages> EncloseOverPageRouter(
        Dictionary<string, NextPages> pageRouter,
        Func<Context, Dictionary<string, NextPages>, AccessibleNextPages> getAllNextStepsDel)
    {
        return c => getAllNextStepsDel(c, pageRouter);
    }


    private static GetNextStepFuncTemplate Compose(
        Func<Context, AccessibleNextPages> f1,
        Func<AccessibleNextPages , string, PageNameAndLogic, PageNameAndLogic?> f2)
    {
        return (cont, bkStep) => f2(f1(cont), cont.pageName, bkStep);
    }


    public static GetNextStepFuncTemplate GetNextStepFunc(PageRouter pageRouter)
    {
        var getAllPossibleNextStepsDel = EncloseOverPageRouter(pageRouter, GetAllAccessibleNextPages);
        var selectNextStepDel = SelectNextStep;

        var getNextStepDel = Compose(getAllPossibleNextStepsDel, selectNextStepDel);

        return getNextStepDel;
    }

}


