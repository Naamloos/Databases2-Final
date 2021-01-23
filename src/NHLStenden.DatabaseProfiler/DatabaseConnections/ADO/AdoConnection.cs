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

        public void Insert(int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                var cmd = new SqlCommand("INSERT INTO Series (Title, Description, IsFilm, AgeRestriction) VALUES ('Lorem Ipsum', 'Lorem Ipsum Doner Kebab', 1, 12)", this.connection);
                cmd.ExecuteNonQuery();
            }
            for (int i = 1; i <= amount; i++)
            {
                var cmd = new SqlCommand("INSERT INTO Genre (GenreName) VALUES ('Creepy Movie :s')", this.connection);
                cmd.ExecuteNonQuery();
            }
            for (int i = 1; i <= amount; i++)
            {
                var cmd = new SqlCommand($"INSERT INTO Series_Genre (SeriesId, GenreId) VALUES ({i}, {i})", this.connection);
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int amount)
        {
            var cmd = new SqlCommand($"DELETE FROM Series_Genre WHERE SeriesId BETWEEN 1 AND {amount}", this.connection);
            cmd.ExecuteNonQuery();
            var cmd2 = new SqlCommand($"DELETE FROM Series WHERE Id BETWEEN 1 AND {amount}", this.connection);
            cmd2.ExecuteNonQuery();
            var cmd3 = new SqlCommand($"DELETE FROM Genre WHERE Id BETWEEN 1 AND {amount}", this.connection);
            cmd3.ExecuteNonQuery();
            var cmd4 = new SqlCommand($"DBCC CHECKIDENT(Series,RESEED,0);DBCC CHECKIDENT(Genre,RESEED,0);", this.connection);
            cmd4.ExecuteNonQuery();
        }

        public void Select(int amount)
        {
            var cmd = new SqlCommand($"SELECT TOP({amount}) * FROM Series_Genre", this.connection);
            cmd.ExecuteNonQuery();
        }

        public void Update(int amount)
        {
            var cmd = new SqlCommand($"UPDATE Series SET Title = 'Lorem Ipsum Kebab'", this.connection);
            cmd.ExecuteNonQuery();
        }

        public string GetName() => $"ADO.NET (SQL Server)";

        public bool IsConnected() => this.connected;
    }
}
