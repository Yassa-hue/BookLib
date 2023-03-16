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
            
            
            // prepare getNextStepFunc
            var getNextStepDel = NextStep.GetNextStepFunc(GetPageRouter());
            
            // prepare openPageFunc
            var openPageDel = Page.CloseOnContextAndNextPageFunc(context, getNextStepDel);
            
            // prepare first page
            var firstPage = FirstPage.GetFirstPage();
            context.pageName = firstPage.pageName;

            // open first page
            openPageDel(firstPage.pageLogic);
            
        }
        
        private static Dictionary<string, List<NextPage>> GetPageRouter()
        {

            var pageRouter = new Dictionary<string, List<NextPage>>();
            
            pageRouter.Add(FirstPage.PageName, FirstPage.GetFirstPageNextChoices());
            pageRouter.Add(LogIn.PageName, LogIn.GetLogInNextChoices());
            pageRouter.Add(SignUp.PageName, SignUp.GetSignUpNextChoices());
            pageRouter.Add(HomePage.PageName, HomePage.GetNextNextChoices());
            pageRouter.Add(ListBooksPage.PageName, ListBooksPage.GetListBooksNextChoices());
            pageRouter.Add(InfoPage.PageName, InfoPage.GetNextSteps());
            pageRouter.Add(BookDetailsPage.PageName, BookDetailsPage.GetBookDetailsNextChoices());
            pageRouter.Add(AddBookPage.PageName, AddBookPage.GetAddBookNextChoices());
            pageRouter.Add(PreferBookPage.PageName, PreferBookPage.GetPreferBookPageNextChoices());
            

            return pageRouter;
        }
        
    }
}