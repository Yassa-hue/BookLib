using System.Data;

namespace BookLib;




public class ListPreferredBooksPage
{
    public const string PageName = "Prefered books page";


    private static List<BookMinorData> GetPreferredBooksFromDb(Context context)
    {
        var dbComm = context.dbConnection.CreateCommand();

        dbComm.CommandText =
            $"select Book.id, Book.title, Book.writer from Book, Prefer where Book.id == Prefer.book_id and Prefer.user_id == '{context.user.id}';";

        var dbReader = dbComm.ExecuteReader();

        var books = new List<BookMinorData>();

        while (dbReader.Read())
        {
            books.Add(new BookMinorData
            {
                id = int.Parse(dbReader.GetString(0)),
                title = dbReader.GetString(1),
                writer = dbReader.GetString(2)
            });            
        }

        return books;
    }


    private static void PrintPreferredBooks(List<BookMinorData> preferredBooks)
    {
        if (preferredBooks.Count == 0)
        {
            Console.WriteLine("No preferred books");
            return;
        }

        foreach (var book in preferredBooks)
        {
            Console.WriteLine(book);
        }
    }

    private static Action<Context> Compose(
        Func<Context, List<BookMinorData>> f1,
        Action<List<BookMinorData>> f2)
    {
        return cont => f2(f1(cont));
    }
    
    private static Action<Context> GetListPreferredBooksLogic()
    {

        var getBooksDel = GetPreferredBooksFromDb;
        var printBooksDel = PrintPreferredBooks;

        var getPreferredBooksDel = Compose(getBooksDel, printBooksDel);
        
        return context =>
        {
            Console.WriteLine(PageName + "\n\n");

            getPreferredBooksDel(context);
        };
    }
    
    
    public static List<NextPage> GetListPreferredBooksPageNextChoices()
    {
        return new List<NextPage>
        {
            BookDetailsPage.GetBookDetailsPage(),
            PreferBookPage.GetPreferBookPage()
        };
    }


    public static NextPage GetListPreferredBooksPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetListPreferredBooksLogic(), adminOnly = false };
    }
}