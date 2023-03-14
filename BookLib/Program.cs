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
            var getNextStepDel = NextStep.GetNextStepFunc(GetPageRouter(context));
            
            // prepare openPageFunc
            var openPageDel = Page.CloseOpenPageFunOnContextAndNextPageFunc(context, getNextStepDel);
            
            // prepare page logic (first page logic)
            var firstPageLogicDel = FirstPage.GetFirstPage(context);

            // open first page
            openPageDel(firstPageLogicDel);
            
        }
        
        public static Dictionary<string, List<(string pageName, Action<Context> funcDel)>> GetPageRouter(Context context)
        {

            var pageRouter = new Dictionary<string, List<(string pageName, Action<Context> funcDel)>>();
            
            pageRouter.Add("First page",   FirstPage.GetFirstPageNextChoices(context));
            pageRouter.Add("Log in page",  LogIn.GetLogInNextChoices(context));
            pageRouter.Add("Sign up page", SignUp.GetSignUpNextChoices(context));
            pageRouter.Add("List books page", ListBooksPage.GetListBooksNextChoices(context));
            

            return pageRouter;
        }
        
    }
}