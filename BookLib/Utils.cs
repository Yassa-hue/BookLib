using Microsoft.Data.Sqlite;



namespace BookLib;

public class Context
{ 
    public User user { get; set; }
    public SqliteConnection dbConnection { get; set; }
}

public static class Utils
{
    public static Func<T1, T3> compose<T1, T2, T3>(this Func<T1, T2> f1, Func<T2, T3> f2)
    {
        return (x) => f2(f1(x));
    }
    
    public static Func<T1, T2> composeWithDbConn<T1, T2>(this Func<T1, SqliteConnection, T2> f, SqliteConnection dbConn)
    {
        return (x) => f(x, dbConn);
    }


    public static SqliteConnection CreateDbConnection(string dbPath)
    {
        var connection = new SqliteConnection(dbPath);

        connection.Open();

        return connection;
    }

    
}