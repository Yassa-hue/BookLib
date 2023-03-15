using System.Linq;

namespace BookLib;


using NextPages = List<NextPage>;
using AccessibleNextPages = List<(string pageName, Action<Context> funcDel)>;

public static class NextStep
{

    
    private static AccessibleNextPages GetAllAccessibleNextPages(Context context, Dictionary<string, NextPages> pageRouter)
    {
        // Filter pages by accessibility
        var accessibleNextPages = pageRouter[context.pageName]
            .Where(page => !page.adminOnly || page.adminOnly == context.user.isAdmin)
            .Select(page => (page.pageName, page.pageLogic)).ToList();

        return accessibleNextPages;
    }
    
    private static (string pageName, Action<Context> funcDel)? SelectNextStep(AccessibleNextPages allPossibleNextSteps, 
        (string pageName, Action<Context> funcDel) backStep)
    {
        // take the user choice and turn it into page
        Console.WriteLine("What is your next step ?");
        
        Console.WriteLine("-2 ) Exit program");
        Console.WriteLine("-1 ) Back step");

        var selectChoices = allPossibleNextSteps
            .Select((step, index) => index + " ) " + step.pageName)
            .ToList();

        foreach (var choice in selectChoices)
        {
            Console.WriteLine(choice);
        }

        var userChoice = int.Parse(Console.ReadLine());

        if (userChoice == -2)
        {
            return null;
        }

        if (userChoice == -1)
        {
            return backStep;
        }

        return allPossibleNextSteps[userChoice];
    }


    private static Func<Context, AccessibleNextPages> EncloseOverPageRouter(
        Dictionary<string, NextPages> pageRouter,
        Func<Context, Dictionary<string, NextPages>, AccessibleNextPages> getAllNextStepsDel)
    {
        return c => getAllNextStepsDel(c, pageRouter);
    }


    private static Func<TCont, TBackStep, TNextStep> Compose<TCont, TStepsList, TBackStep, TNextStep>(Func<TCont, TStepsList> f1,
        Func<TStepsList, TBackStep, TNextStep> f2)
    {
        return (cont, bkStep) => f2(f1(cont), bkStep);
    }


    public static Func<Context, (string pageName, Action<Context> funcDel), (string pageName, Action<Context> funcDel)?> GetNextStepFunc(Dictionary<string, NextPages> pageRouter)
    {
        var getAllPossibleNextStepsDel = EncloseOverPageRouter(pageRouter, GetAllAccessibleNextPages);
        var selectNextStepDel = SelectNextStep;

        var getNextStepDel = Compose(getAllPossibleNextStepsDel, selectNextStepDel);

        return getNextStepDel;
    }

}


