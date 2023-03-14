using Microsoft.Data.Sqlite;

namespace BookLib;

public class Context
{ 
    public User user { get; set; }
    public SqliteConnection dbConnection { get; set; }

    public string pageName { get; set; }
}


public class NextPage
{
    public string pageName { get; set; }
    public Action<Context> pageLogic { get; set; }
    public bool adminOnly { get; set; }
}

public static class Utils
{
    public static Func<T1, T3> Compose<T1, T2, T3>(this Func<T1, T2> f1, Func<T2, T3> f2)
    {
        return (x) => f2(f1(x));
    }
    
    public static Action<T1> Compose<T1, T2>(this Func<T1, T2> f1, Action<T2> f2)
    {
        return x => f2(f1(x));
    }
    
    public static Func<T3> Compose<T2, T3>(this Func<T2> f1, Func<T2, T3> f2)
    {
        return () => f2(f1());
    }
    
    public static Func<T1, T2> ComposeWithDbConn<T1, T2>(this Func<T1, SqliteConnection, T2> f, SqliteConnection dbConn)
    {
        return (x) => f(x, dbConn);
    }


    public static SqliteConnection CreateDbConnection(string dbPath)
    {
        string dbAbsPath = Path.Combine(Environment.CurrentDirectory, dbPath);
        
        var connection = new SqliteConnection($@"Data Source={dbAbsPath}");

        connection.Open();

        return connection;
    }

    
}