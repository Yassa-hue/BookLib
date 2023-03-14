using Microsoft.Data.Sqlite;

namespace BookLib;

public class ListBooksPage
{

    public static List<Book> GetBooks(SqliteConnection dbConn)
    {
        // to be implemented
        return new List<Book>();
    }

    public static void PrintBooks(List<Book> books)
    {
        // to be implemented
    }

    public static Action<Context> GetListBooks()
    {
        var getBooksDel = GetBooks;
        var printBooksDel = PrintBooks;
        
        return context => getBooksDel.Compose(printBooksDel);
    }
    
    
    public static List<NextPage> GetListBooksNextChoices()
    {
        return new List<NextPage>
        {
            
        };
    }



}


public class Book
{
    public int id { get; set; }
    public string title { get; set; }
    public string writer { get; set; }
    public string publisher { get; set; }
    public int pages { get; set; }
    public string owner { get; set; }
}