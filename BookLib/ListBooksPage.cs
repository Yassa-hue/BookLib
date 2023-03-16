using Microsoft.Data.Sqlite;

namespace BookLib;

public class ListBooksPage
{


    public const string PageName = "List Books Page";
    private static List<BookMinorData> GetBooksFromDb(Context context)
    {
        var dbCom = context.dbConnection.CreateCommand();

        dbCom.CommandText = "SELECT id, title, writer FROM Book LIMIT 10;";
        
        var books = new List<BookMinorData>();

        var dbReader = dbCom.ExecuteReader();
        
        while (dbReader.Read())
        {
            var book = new BookMinorData
            {
                id = int.Parse(dbReader.GetString(0)),
                title = dbReader.GetString(1),
                writer = dbReader.GetString(2)
            };
            
            books.Add(book);
        }
        
        
        return books;
    }

    private static void PrintBooks(List<BookMinorData> books)
    {
        foreach (var book in books)
        {
            Console.WriteLine("Book ID : " + book.id + ", Title : " + book.title + ", Writer : " + book.writer);
        }
    }

    

    public static Action<Context> GetListBooksPageLogic()
    {
        var getBooksDel = GetBooksFromDb;
        var printBooksDel = PrintBooks;
        var listBooksDel = getBooksDel.Compose(printBooksDel);

        return cont =>
        {
            Console.WriteLine(PageName + "\n\n");
            listBooksDel(cont);
        };
    }
    
    
    public static List<NextPage> GetListBooksNextChoices()
    {
        return new List<NextPage>
        {
            BookDetailsPage.GetBookDetailsPage(),
            PreferBookPage.GetPreferBookPage()
        };
    }


    public static NextPage GetListBooksPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetListBooksPageLogic(), adminOnly = false };
    }

}


public class BookMinorData
{
    public int id { get; set; }
    public string title { get; set; }
    public string writer { get; set; }

    public override string ToString()
    {
        return "ID : " + id + ", Title : " + title + ", Writer : " + writer;
    }
}