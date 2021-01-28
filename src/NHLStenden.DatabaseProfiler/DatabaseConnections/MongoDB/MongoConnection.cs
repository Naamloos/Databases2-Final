using NHLStenden.DatabaseProfiler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction;

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
            var version = this.database.RunCommand(new BsonDocumentCommand<BsonDocument>(new BsonDocument() { { "buildInfo", 1 } }))["version"];
            this.connected = true;
            this.logger.LogMessage($"Connected to a MongoDB Server\n  - MongoDB Server version: {version}.", true);
        }

        public void Insert(int amount)
        {
            throw new NotImplementedException();
        }

        public void Delete(int amount)
        {
            throw new NotImplementedException();
        }

        public void Select(int amount)
        {
            throw new NotImplementedException();
        }

        public void Update(int amount)
        {
            throw new NotImplementedException();
        }

        public string GetName() => $"MongoDB";

        public bool IsConnected() => this.connected;
    }
}
