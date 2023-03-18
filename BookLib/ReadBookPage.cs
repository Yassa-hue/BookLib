using System.Data;

namespace BookLib;

public class ReadBookPage
{
    public const string PageName = "Read book page";


    private static int GetBookId()
    {
        Console.WriteLine("Enter the id of the book to read :");

        return int.Parse(Console.ReadLine());
    }


    private static ReadingSession CreateReadingSession(Context context, int bookId)
    {
        var dbComm = context.dbConnection.CreateCommand();

        dbComm.CommandText = $"insert into Read values('{context.user.id}', '{bookId}', 1);";

        dbComm.ExecuteNonQuery();

        dbComm.CommandText = $"select title, pages from Book where id == '{bookId}';";

        var dbReader = dbComm.ExecuteReader();

        dbReader.Read();

        return new ReadingSession(
            bookId,
            dbReader.GetString(0),
            1,
            int.Parse(dbReader.GetString(1))
        );
    }


    private static ReadingSession GetReadingSessionFromDb(Context context, int bookId)
    {
        var dbComm = context.dbConnection.CreateCommand();
        
        dbComm.CommandText = $"select Book.title, Read.page, Book.pages from Read, Book where Book.id == Read.book_id and Read.user_id == '{context.user.id}';";

        var dbReader = dbComm.ExecuteReader();

        if (!dbReader.Read())
        {
            return CreateReadingSession(context, bookId);
        }

        return new ReadingSession(
            bookId, 
            dbReader.GetString(0),
            int.Parse(dbReader.GetString(1)),
            int.Parse(dbReader.GetString(2)));
    }


    private static ReadingSession UpdateReadingSession(Context context, ReadingSession readingSession)
    {
        var dbComm = context.dbConnection.CreateCommand();

        dbComm.CommandText = $"update Read set page = '{readingSession.pagesNum}' where book_id == '{readingSession.bookId}' and user_id == '{context.user.id}';";

        dbComm.ExecuteNonQuery();

        return readingSession;
    }


    private static Func<Context, ReadingSession> ComposeReadingSessionFunc()
    {
        return cont => GetReadingSessionFromDb(cont, GetBookId());
    }



    private static int GetReaderBrowsingChoice(ReadingSession readingSession)
    {
        Console.WriteLine("To browse the book : ");

        if (readingSession.currentPage > 1) // can move backward
        {
            Console.WriteLine("-1 ) Move to the next page");
        }

        Console.WriteLine("0 ) Exit reading session");
        
        if (readingSession.currentPage < readingSession.pagesNum) // can move forward
        {
            Console.WriteLine("1 ) Move to the next page");
        }

        int browsingChoice = int.Parse(Console.ReadLine());

        if (browsingChoice == 0)
        {
            throw new Exception("Exit reading session");
        }

        return browsingChoice;
    }


    private static ReadingSession UpdateCurrentPageNum(ReadingSession readingSession, int browsingChoice)
    {
        return readingSession with { currentPage = readingSession.currentPage + browsingChoice };
    }


    private static Func<Context, ReadingSession, ReadingSession> ComposeUpdateReadingSessionFunc()
    {
        var takeReaderChoiceDel = GetReaderBrowsingChoice;
        var updatePageNumDel = UpdateCurrentPageNum;
        var updateSessionDel = UpdateReadingSession;

        return (cont, readingSes) =>
            updateSessionDel(cont, updatePageNumDel(readingSes, takeReaderChoiceDel(readingSes)));
    }


    private static void ReadBook(Context context)
    {
        var getSessionDel = ComposeReadingSessionFunc();
        var updateSessionDel = ComposeUpdateReadingSessionFunc();


        var readingSession = getSessionDel(context);
        Console.WriteLine("Book title : " + readingSession.bookTitle + ", Pages number : " + readingSession.pagesNum);

        while (true)
        {
            Console.WriteLine("Page number : " + readingSession.currentPage);

            try
            {
                readingSession = updateSessionDel(context, readingSession);
            }
            catch (Exception e) // flag to exit the reading session 
            {
                break;
            }
        }
    }



    public static Action<Context> GetReadBookPageLogic()
    {
        return cont =>
        {
            Console.WriteLine("Read book page\n\n");

            ReadBook(cont);
        };
    }
    
    
    public static List<NextPage> GetReadBookPageNextSteps()
    {
        var nextSteps = new List<NextPage>
        {
            ListPreferredBooksPage.GetListPreferredBooksPage()
        };

        return nextSteps;
    }

    public static NextPage GetReadBookPage()
    {
        return new NextPage { pageName = PageName, pageLogic = GetReadBookPageLogic(), adminOnly = false };
    }
}


public record ReadingSession(int bookId, string bookTitle, int currentPage, int pagesNum);