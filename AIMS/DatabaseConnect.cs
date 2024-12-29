using Npgsql;
using System.Data;
using System.Threading.Tasks;

public class DatabaseConnect
{
    private static DatabaseConnect _Instance;
    public static DatabaseConnect gI()
    {
        if (_Instance == null)
        {
            _Instance = new DatabaseConnect();
        }
        return _Instance;
    }

    public string ConnectionString = "User ID=postgres.dwsijitgwefuomejoime;Password=9Tb9eeaw1vsmClOd;Host=aws-0-ap-southeast-1.pooler.supabase.com;Port=6543;Database=postgres;Pooling=true;Timeout=60;CommandTimeout=60;Connection Lifetime=60;Multiplexing=true";
    public NpgsqlConnection Connection;

    public async Task ConnectAsync()
    {
        if (Connection == null)
            Connection = new NpgsqlConnection(ConnectionString);

        if (Connection.State == ConnectionState.Closed)
            await Connection.OpenAsync();
    }

    public async Task CloseConnectionAsync()
    {
        if (Connection != null && Connection.State == ConnectionState.Open)
            await Connection.CloseAsync();
    }
}
