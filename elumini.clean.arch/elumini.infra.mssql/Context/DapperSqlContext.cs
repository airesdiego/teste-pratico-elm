using System.Data;
using System.Data.SqlClient;

namespace elumini.infra.mssql.Context;

public class DapperSqlContext : IDisposable
{
    private readonly SqlConnection _connection;

    public DapperSqlContext(SqlConnection connection)
    {
        _connection = connection;
        OpenConnection();
    }

    public SqlConnection Connection => _connection;


    private void OpenConnection()
    {
        if (Connection.State != ConnectionState.Open)
        {
            Connection.Open();
        }
    }

    public void Dispose() => Connection.Close();
}