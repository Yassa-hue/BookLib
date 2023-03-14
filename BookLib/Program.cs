using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace BookLib
{
    static class MainClass
    {
        static void Main()
        {

            // prepare context
            Context context = new Context();
            context.dbConnection = Utils.CreateDbConnection("book.db");
            context.pageName = "First page";
            
            // prepare getNextStepFunc
            var getNextStepDel = NextStep.GetNextStepFunc(GetPageRouter());
            
            // prepare openPageFunc
            var openPageDel = Page.CloseOpenPageFunOnContextAndNextPageFunc(context, getNextStepDel);
            
            // prepare page logic (first page logic)
            var firstPageLogicDel = FirstPage.GetFirstPage(context);

            // open first page
            openPageDel(firstPageLogicDel);
            
        }
        
        private static Dictionary<string, List<NextPage>> GetPageRouter()
        {

            var pageRouter = new Dictionary<string, List<NextPage>>();
            
            pageRouter.Add("First page",   FirstPage.GetFirstPageNextChoices());
            pageRouter.Add("Log in page",  LogIn.GetLogInNextChoices());
            pageRouter.Add("Sign up page", SignUp.GetSignUpNextChoices());
            pageRouter.Add("Home page up page", HomePage.GetNextsteps());
            pageRouter.Add("List books page", ListBooksPage.GetListBooksNextChoices());
            

            return pageRouter;
        }
        
    }
}

/*
  'System.Func<BookLib.Context, (string pageName, System.Action<BookLib.Context> funcDel), (string pageName, System.Action<BookLib.Context> funcDel)?>' 
  'System.Func<BookLib.Context, System.Action<BookLib.Context>, (string pageName, System.Action<BookLib.Context> funcDel)?>'
*/