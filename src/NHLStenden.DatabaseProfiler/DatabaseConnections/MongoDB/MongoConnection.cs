using NHLStenden.DatabaseProfiler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction;
using NHLStenden.DatabaseProfiler.DatabaseConnections.MongoDB.Entities;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.MongoDB
{
    public class MongoConnection : IDatabaseConnection
    {
        private Logger logger;
        private MongoClient connection;
        private IMongoDatabase database;
        private bool connected = false;
        private Config config;

        public MongoConnection(Logger logger, Config config)
        {
            this.logger = logger;
            this.config = config;
            this.connection = new MongoClient(config.MongoConnectionString);
        }

        public void Connect()
        {
            this.connection.StartSession();
            this.database = this.connection.GetDatabase(config.MongoDatabaseName);
            var collections = this.database.ListCollectionNames().ToList();

            if (!collections.Contains("Series"))
                this.database.CreateCollection("Series");

            var version = this.database.RunCommand(new BsonDocumentCommand<BsonDocument>(new BsonDocument() { { "buildInfo", 1 } }))["version"];
            this.connected = true;
            this.logger.LogMessage($"Connected to a MongoDB Server\n  - MongoDB Server version: {version}.", true);
        }

        public void Insert(int amount)
        {
            var seriescollection = this.database.GetCollection<Series>("Series");
            List<Series> series = new List<Series>();

            for(int i = 0; i < amount; i++)
            {
                var s = new Series()
                {
                    Id = i,
                    IsFilm = true,
                    AgeRestriction = 12,
                    Description = "Lorem Ipsum Doner Kebab",
                    Title = "Lorem Ipsum",
                };

                s.Genres.Add(new Genre()
                {
                    Name = "Creepy Movie :s"
                });

                series.Add(s);
            }

            seriescollection.InsertMany(series);
        }

        public void Delete(int amount)
        {
            var seriescollection = this.database.GetCollection<Series>("Series");
            seriescollection.DeleteMany(x => x.Id < amount);
        }

        public void Select(int amount)
        {
            var seriescollection = this.database.GetCollection<Series>("Series");
            seriescollection.Find<Series>(x => x.Id < amount);
        }

        public void Update(int amount)
        {
            var seriescollection = this.database.GetCollection<Series>("Series");
            seriescollection.UpdateMany<Series>(x => x.Id < amount, "{ $set: { \"Title\": \"Lorem Ipsum Kebab\"}}");
        }

        public string GetName() => $"MongoDB";

        public bool IsConnected() => this.connected;
    }
}
