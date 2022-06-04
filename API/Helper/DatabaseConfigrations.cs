namespace API.Helper
{
    public class DatabaseConfigrations
    {
        public static string ConnectionString { get; private set; } = string.Empty;
        public static void SetConnectionString(string connection)
        {
            ConnectionString = connection;
        }
    }
}
