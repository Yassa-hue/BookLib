namespace BookLib;

public class AddBookPage
{
    public const string PageName = "Add book page";


    private static AddingBookData TakeBookDataFromUser()
    {
        Console.WriteLine("Enter book data : title, writer, publisher and number of pages (each on a separate line)");

        string title = Console.ReadLine();
        string writer = Console.ReadLine();
        string publisher = Console.ReadLine();
        int numOfPages =  int.Parse(Console.ReadLine());

        return new AddingBookData(title, writer, publisher, numOfPages);
    }


    private static void InsertBookIntoDb(Context context, AddingBookData bookData)
    {
        var dbCommand = context.dbConnection.CreateCommand();
        
        dbCommand.CommandText = $"INSERT INTO Book(title, writer, publisher, pages, owner_id) VALUES('{bookData.title}', '{bookData.writer}', '{bookData.publisher}', '{bookData.pages}', '{context.user.id}');";
        dbCommand.ExecuteNonQuery();
    }


    private static Action<Context> Compose(Func<AddingBookData> f1, Action<Context, AddingBookData> f2)
    {
        return context => f2(context, f1());
    }


    public static Action<Context> GetAddBookPageLogic()
    {
        var takeDataDel = TakeBookDataFromUser;
        var insertBookDel = InsertBookIntoDb;

        var addNewBookDel = Compose(takeDataDel, insertBookDel);

        return cont =>
        {
            Console.WriteLine("Add new book page\n\n");

            addNewBookDel(cont);
        };
    }
    
    
    public static List<NextPage> GetAddBookNextChoices()
    {
        return new List<NextPage>
        {
            
        };
    }

    public static NextPage GetAddBookPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetAddBookPageLogic(), adminOnly = true };
    }
}


public record AddingBookData (string title, string writer, string publisher, int pages);