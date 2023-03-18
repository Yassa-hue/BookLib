namespace BookLib;

public class PreferBookPage
{
    public const string PageName = "Prefer book page";

    private static int TakeBookIdFromUser()
    {
        Console.WriteLine("Write the book id you want to a add to your prefer collection:");

        int bookId = int.Parse(Console.ReadLine());

        return bookId;
    }


    private static int AddBookToPrefers(int bookId, Context context)
    {
        // add book to prefers
        var dbComm = context.dbConnection.CreateCommand();
        dbComm.CommandText = $"insert into Prefer(user_id, book_id) values ('{context.user.id}', '{bookId}')";
        dbComm.ExecuteNonQuery();

        return bookId;
    }

    private static Action<Context> Compose(
        Func<int> f1,
        Func<int, Context, int> f2)
    {
        return cont => f2(f1(), cont);
    }
    
    
    public static Action<Context> GetPreferBookLogic()
    {

        var getBookIdDel = TakeBookIdFromUser;
        var storeInPrefDel = AddBookToPrefers;

        var preferBookDel = Compose(getBookIdDel, storeInPrefDel);
        
        return context =>
        {
            
            Console.WriteLine(PageName + "\n\n");

            preferBookDel(context);
            
            Console.WriteLine("Added successfully");

        };
    }


    public static List<NextPage> GetPreferBookPageNextChoices()
    {
        return new List<NextPage>
        {
            BookDetailsPage.GetBookDetailsPage(),
            ListPreferredBooksPage.GetListPreferredBooksPage(),
            ReadBookPage.GetReadBookPage()
        };
    }
    
    
    
    public static NextPage GetPreferBookPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetPreferBookLogic(), adminOnly = false };
    }
}