using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

class SimpleDataSource : IDisposable
{
    MySqlConnection Conn;

    /// <summary>
    /// Constructor, calls Connect method with params
    /// </summary>
    /// <param name="Server">Server IP or hostname</param>
    /// <param name="Database">Database/schema name</param>
    /// <param name="Port">Port number</param>
    /// <param name="User">Username</param>
    /// <param name="Password">Password</param>
    public SimpleDataSource(string Server, string Database, int Port,
        string User, string Password)
    {
        Connect(Server, Database, Port, User, Password);
    }

    /// <summary>
    /// Intialises MySqlConnection object with parameters provided.
    /// </summary>
    /// <param name="Server">Server IP or hostname</param>
    /// <param name="Database">Database/schema name</param>
    /// <param name="Port">Port number</param>
    /// <param name="User">Username</param>
    /// <param name="Password">Password</param>
    public void Connect(string Server, string Database, int Port,
        string User, string Password)
    {
        string ConnectString = string.Format("server={0};user={1};database={2};port={3};password={4};",
                        Server, User, Database, Port, Password);
        try
        {
            Conn = new MySqlConnection(ConnectString);
            Conn.Open();
        }
        catch (MySqlException E)
        {
            Console.WriteLine("A MySQL Exception Occured: \n" + E.Message);
            Conn = null;
        }
    }

    /// <summary>
    /// Creates an SQL query from the provided string and
    /// executes it.
    /// </summary>
    /// <param name="QueryString">A string containing an SQL query</param>
    /// <returns>A MySqlDataReader object with the results 
    /// of the query</returns>
    public MySqlDataReader Query(string QueryString)
    {
        MySqlCommand Command = null;
        MySqlDataReader Reader = null;

        if (Conn != null)
        {
            lock (Conn)
            {
                try
                {
                    Command = new MySqlCommand(QueryString, Conn);
                    Reader = Command.ExecuteReader();
                }
                catch (MySqlException E)
                {
                    Console.WriteLine("A MySQL Exception Occured: \n " + E.Message);
                }
            }
        }

        return Reader;
    }


    /// <summary>
    /// Creates an SQL query from the provided string and
    /// executes it with the parameters specified in Params.
    /// </summary>
    /// <param name="QueryString">A string containing an SQL query</param>
    /// <param name="Params">A Dictionary of parameters</param>
    /// <returns>A MySqlDataReader object with the results 
    /// of the query</returns>
    public MySqlDataReader QueryPreparedStatement(String QueryString, Dictionary<string, string> Params)
    {
        MySqlCommand Command = null;
        MySqlDataReader Reader = null;

        if (Conn != null)
        {
            lock (Conn)
            {
                try
                {
                    Command = new MySqlCommand(QueryString, Conn);
                    foreach (KeyValuePair<string, string> Item in Params)
                    {
                        Command.Parameters.AddWithValue(Item.Key, Item.Value);
                    }
                    Reader = Command.ExecuteReader();
                }
                catch (MySqlException E)
                {
                    Console.WriteLine("A MySQL Exception Occured: \n " + E.Message);
                }
            }
        }

        return Reader;
    }


    /// <summary>
    /// Creates an SQL query from the provided string and
    /// executes it with the parameters specified in Params.
    /// </summary>
    /// <param name="QueryString">A string containing an SQL query</param>
    /// <param name="Params">A Dictionary of parameters</param>
    /// <returns>A DataTable containing the results or null if the query didn't run.</returns>
    public DataTable QueryDataTable(string QueryString, Dictionary<string, string> Params)
    {
        DataTable Table = null;
        MySqlCommand Command = null;

        if (Conn != null)
        {
            lock (Conn)
            {
                try
                {
                    Command = new MySqlCommand(QueryString, Conn);
                    foreach (KeyValuePair<string, string> Item in Params)
                    {
                        Command.Parameters.AddWithValue(Item.Key, Item.Value);
                    }
                    MySqlDataAdapter Adapter = new MySqlDataAdapter(Command);
                    Table = new DataTable();
                    Adapter.Fill(Table);
                }
                catch (MySqlException E)
                {
                    Console.WriteLine("A MySQL Exception Occured: \n " + E.Message);
                }
            }
        }

        return Table;
    }

    /// <summary>
    /// Creates an SQL statement from the provided string and
    /// executes it. 
    /// </summary>
    /// <param name="UpdateString">A string containing an SQL non-query</param>
    public void Update(string UpdateString)
    {
        MySqlCommand Command = null;

        if (Conn != null)
        {
            lock (Conn)
            {
                try
                {
                    Command = new MySqlCommand(UpdateString, Conn);
                    Command.ExecuteNonQuery();
                }
                catch (MySqlException E)
                {
                    Console.WriteLine("A MySQL Exception Occured: \n " + E.Message);
                }
            }
        }
    }

    /// <summary>
    /// Creates an SQL statement from the provided string and
    /// executes it. 
    /// </summary>
    /// <param name="UpdateString">A string containing an SQL non-query</param>
    /// <param name="Params">A Dictonary of parameters</param>
    public void UpdatePreparedStatement(string UpdateString, Dictionary<string, string> Params)
    {
        MySqlCommand Command = null;

        if (Conn != null)
        {
            lock (Conn)
            {
                try
                {
                    Command = new MySqlCommand(UpdateString, Conn);
                    foreach (KeyValuePair<string, string> Item in Params)
                    {
                        Command.Parameters.AddWithValue(Item.Key, Item.Value);
                    }
                    Command.ExecuteNonQuery();
                }
                catch (MySqlException E)
                {
                    Console.WriteLine("A MySQL Exception Occured: \n " + E.Message);
                }
            }
        }
    }

    /// <summary>
    /// Garbage collection method called by Garbage Collector.
    /// SimpleDataSource implements IDisposable, so the GC will
    /// know to call this method when the object is no longer
    /// needed.
    /// </summary>
    public void Dispose()
    {
        if (Conn != null)
            Conn.Dispose();
    }
}