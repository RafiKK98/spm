using MySql.Data.MySqlClient;

namespace SpmsApp.Services
{
    public class DataServices
    {
        // public static DataServices Get { get; private set; }
        private static DataServices _ds = new DataServices();

        private MySqlConnection connection;
        private MySqlCommand command;

        private DataServices()
        {
            connection = new MySqlConnection("server=localhost;database=spmsdb;userid=spms;password=");
        }

        public static DataServices Get { get => _ds; }

        public MySqlDataReader RunQuery(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            var reader = command.ExecuteReader();
            connection.Close();
            return reader;
        }

        public int RunUpdate(string query)
        {
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = query;
            var result = command.ExecuteNonQuery();
            connection.Close();
            return result;
        }
    }
}