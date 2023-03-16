namespace BookLib;

public class BookDetailsPage
{
    public const string PageName = "Book details page";

    private static int GetBookIdFromUser()
    {
        Console.WriteLine("Enter the book id (integer value)");
        int bookId = int.Parse(Console.ReadLine());

        return bookId;
    }


    private static BookDetails GetBookDetails(Context context, int bookId)
    {
        var dbComm = context.dbConnection.CreateCommand();

        dbComm.CommandText = $"SELECT Book.id, Book.title, Book.writer, Book.publisher, Book.pages, User.name FROM Book, User where Book.id == '{bookId}' AND Book.owner_id == User.id;";

        var dbReader = dbComm.ExecuteReader();

        if (!dbReader.HasRows)
        {
            throw new Exception("This id is not found");
        }

        dbReader.Read();

        return new BookDetails(
            int.Parse(dbReader.GetString(0)),
            dbReader.GetString(1),
            dbReader.GetString(2),
            dbReader.GetString(3),
            int.Parse(dbReader.GetString(4)),
            dbReader.GetString(5)
        );
    }

    private static void PrintBookDetails(BookDetails bookDetails)
    {
        Console.WriteLine("ID : " + bookDetails.id);
        Console.WriteLine("Title : " + bookDetails.title);
        Console.WriteLine("Writer : " + bookDetails.writer);
        Console.WriteLine("Publisher : " + bookDetails.publisher);
        Console.WriteLine("Number of pages : " + bookDetails.pages);
        Console.WriteLine("Owner : " + bookDetails.owner);
    }


    private static Action<Context> GetBookDetailsLogic(Context context)
    {

        var getBookIdDel = GetBookIdFromUser;
        var getBookDetailsWithContext = GetBookDetails;
        var getBookDetailsDel = Utils.ComposeWithContext(getBookDetailsWithContext, context);
        var printBookDetailsDel = PrintBookDetails;
        
        var bookDetails = getBookIdDel.Compose(getBookDetailsDel).Compose(printBookDetailsDel);

        return context =>
        {
            Console.WriteLine(PageName + "\n\n");
            const int maxTries = 3;
            var triesNum = 0;
            while (triesNum < maxTries)
            {
                try
                {
                    bookDetails();
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error : " + e.Message);
                }

                triesNum+=1;
            }
        };
    }
    
    
    public static List<NextPage> GetBookDetailsNextChoices()
    {
        return new List<NextPage>
        {
            
        };
    }


    public static NextPage GetBookDetailsPage(Context context)
    {
        return new NextPage { pageName = PageName, pageLogic = GetBookDetailsLogic(context), adminOnly = false };
    }
    
    
    
}


public record BookDetails(int id, string title, string writer, string publisher, int pages, string owner);