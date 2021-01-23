using NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction;
using NHLStenden.DatabaseProfiler.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.ADO
{
    public class AdoConnection : IDatabaseConnection
    {
        private Logger logger;
        private SqlConnection connection;
        private bool connected = false;

        public AdoConnection(Logger logger)
        {
            this.logger = logger;
            this.connection = new SqlConnection(Constants.ADO_CONNECTION_STRING);
        }

        public void Connect()
        {
            this.connection.Open();
            this.connection.ChangeDatabase(Constants.ADO_DATABASE_NAME);
            this.connected = true;
            this.logger.LogMessage($"Connected to an ADO.NET SQL Server\n  - SQL Server version: {connection.ServerVersion}.", true);
        }

        public void Create(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var cmd = new SqlCommand("INSERT INTO Series (Title, Description, IsFilm, AgeRestriction) VALUES ('Lorem Ipsum', 'Lorem Ipsum Doner Kebab', 1, 12)", this.connection);
                cmd.ExecuteNonQuery();
            }
            for (int i = 0; i < amount; i++)
            {
                var cmd = new SqlCommand("INSERT INTO Genre (GenreName) VALUES ('Creepy Movie :s')", this.connection);
                cmd.ExecuteNonQuery();
            }
            for (int i = 0; i < amount; i++)
            {
                var cmd = new SqlCommand($"INSERT INTO Series_Genre (SeriesId, GenreId) VALUES ({i}, {i})", this.connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int amount)
        {
            var cmd = new SqlCommand($"DELETE FROM Series WHERE Id BETWEEN 0 AND {amount - 1}", this.connection);
            cmd.ExecuteNonQuery();
        }

        public void Select(int amount)
        {
            var cmd = new SqlCommand($"SELECT * FROM Series_Genre LIMIT {amount}", this.connection);
            cmd.ExecuteNonQuery();
        }

        public void Update(int amount)
        {
            var cmd = new SqlCommand($"UPDATE Series SET Title = 'Lorem Ipsum Kebab'");
            cmd.ExecuteNonQuery();
        }

        public string GetName() => $"ADO.NET (SQL Server)";

        public bool IsConnected() => this.connected;
    }
}
