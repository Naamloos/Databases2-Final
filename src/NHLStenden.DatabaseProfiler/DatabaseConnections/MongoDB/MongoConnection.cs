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

        public MongoConnection(Logger logger)
        {
            this.logger = logger;
            this.connection = new MongoClient(Constants.MONGO_CONNECTION_STRING);
        }

        public void Connect()
        {
            this.connection.StartSession();
            this.database = this.connection.GetDatabase(Constants.MONGO_DATABASE_NAME);
            var version = this.database.RunCommand(new BsonDocumentCommand<BsonDocument>(new BsonDocument() { { "buildInfo", 1 } }))["version"];
            this.connected = true;
            this.logger.LogMessage($"Connected to a MongoDB Server\n  - MongoDB Server version: {version}.", true);
        }

        public void Create(int amount)
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
