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

            Context context = new Context();
            context.dbConnection = Utils.CreateDbConnection("");
            context.pageName = "First page";
            
            Page.OpenPage(context, FirstPage.GetFirstPage(context));
        }
        
    }
}