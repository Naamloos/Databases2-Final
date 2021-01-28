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
        private Config config;

        public AdoConnection(Logger logger, Config config)
        {
            this.logger = logger;
            this.config = config;
            this.connection = new SqlConnection(config.AdoNetConnectionString);
        }

        public void Connect()
        {
            this.connection.Open();
            this.connection.ChangeDatabase(config.AdoNetDatabaseName);
            this.connected = true;
            this.logger.LogMessage($"Connected to an ADO.NET SQL Server\n  - SQL Server version: {connection.ServerVersion}.", true);
        }

        public void Insert(int amount)
        {
            StringBuilder query = new StringBuilder();
            for (int i = 1; i <= amount; i++)
            {
                query.Append("INSERT INTO Series (Title, Description, IsFilm, AgeRestriction) VALUES ('Lorem Ipsum', 'Lorem Ipsum Doner Kebab', 1, 12);");
            }
            this.RunQueryNonresult(query.ToString());
            query.Clear();

            for (int i = 1; i <= amount; i++)
            {
                query.Append("INSERT INTO Genre (GenreName) VALUES ('Creepy Movie :s');");
            }
            this.RunQueryNonresult(query.ToString());
            query.Clear();

            for (int i = 1; i <= amount; i++)
            {
                query.Append($"INSERT INTO Series_Genre(SeriesId, GenreId) VALUES({i}, {i});");
            }
            this.RunQueryNonresult(query.ToString());
            query.Clear();
        }

        public void Delete(int amount)
        {
            this.RunQueryNonresult($"DELETE FROM Series_Genre WHERE SeriesId BETWEEN 1 AND {amount}");
            this.RunQueryNonresult($"DELETE FROM Series WHERE Id BETWEEN 1 AND {amount}");
            this.RunQueryNonresult($"DELETE FROM Genre WHERE Id BETWEEN 1 AND {amount}");
            // Needed so the identity goes back to 1
            this.RunQueryNonresult($"DBCC CHECKIDENT(Series,RESEED,0); DBCC CHECKIDENT(Genre,RESEED,0);");
        }

        public void Select(int amount)
        {
            this.RunQueryNonresult($"SELECT TOP({amount}) * FROM Series_Genre");
        }

        public void Update(int amount)
        {
            this.RunQueryNonresult($"UPDATE Series SET Title = 'Lorem Ipsum Kebab'");
        }

        public string GetName() => $"ADO.NET (SQL Server)";

        public bool IsConnected() => this.connected;

        private void RunQueryNonresult(string query)
        {
            if (string.IsNullOrEmpty(query))
                throw new ArgumentException("Your query is empty ya dingus");

            var cmd = new SqlCommand(query, this.connection);
            cmd.ExecuteNonQuery();
        }
    }
}
