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
            context.dbConnection = Utils.CreateDbConnection("");
            context.pageName = "First page";
            
            // prepare getNextStepFunc
            var getNextStepDel = NextStep.GetNextStepFunc();
            
            // prepare openPageFunc
            var openPageDel = Page.CloseOpenPageFunOnContextAndNextPageFunc(context, getNextStepDel);
            
            // prepare page logic (first page logic)
            var firstPageLogicDel = FirstPage.GetFirstPage(context);

            // open first page
            openPageDel(firstPageLogicDel);
            
        }
        
    }
}