using NHLStenden.DatabaseProfiler.Attributes;
using NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction;
using NHLStenden.DatabaseProfiler.DatabaseConnections.ADO;
using NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework;
using NHLStenden.DatabaseProfiler.DatabaseConnections.MongoDB;
using NHLStenden.DatabaseProfiler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler
{
    /// <summary>
    /// Main profiler class.
    /// </summary>
    public class Profiler
    {
        private Logger logger;
        private ExecutionProfiler profiler;

        // Database connections.
        private IDatabaseConnection adoConnection;
        private IDatabaseConnection mongoConnection;
        private IDatabaseConnection efConnection;

        private Config config;

        public Profiler(Config config)
        {
            this.config = config;
            this.logger = new Logger("NHL Stenden Database Profiler");
            this.profiler = new ExecutionProfiler(this.logger);
            this.adoConnection = new AdoConnection(this.logger, config);
            this.mongoConnection = new MongoConnection(this.logger, config);
            this.efConnection = new EfConnection(this.logger, config);
        }

        /// <summary>
        /// Starts profiling.
        /// </summary>
        public void Start()
        {
            this.logger.LogMessage("This profiler application expects an empty Genre, Series, and Series_Genre table!\nPress any key to continue.");
            Console.ReadKey();

            this.logger.LogMessage("Attempting connection to all databases...");
            this.adoConnection.Connect();
            this.mongoConnection.Connect();
            this.efConnection.Connect();
            this.logger.LogMessage("Done connecting to all databases.");

            this.logger.LogMessage("Registering databases to profiler...");
            this.profiler.RegisterDatabase(this.adoConnection);
            this.profiler.RegisterDatabase(this.mongoConnection);
            this.profiler.RegisterDatabase(this.efConnection);
            this.logger.LogMessage("Done registering databases to profiler.");

            this.logger.LogMessage("Press any key to start profiling...");
            Console.ReadKey();

            this.logger.LogMessage("Starting profiling methods.");

            // use reflection to find all static methods in ProfilerMethods that have 
            // the ProfilerInfo attribute, because it's just way faster than manually inputting it all.
            var methods = typeof(ProfilerMethods).GetMethods()
                .Where(x => x.IsStatic)
                .Where(x => x.CustomAttributes
                    .Any(y => y.AttributeType == typeof(ProfilerInfoAttribute)));

            foreach (var amount in config.amounts)
            {
                foreach (var m in methods)
                {
                    // create a delegate out of the methods we found.
                    var methoddelegate = m.CreateDelegate(typeof(ExecutionProfiler.ProfilableMethod));
                    this.profiler.ProfileExecution((ExecutionProfiler.ProfilableMethod)methoddelegate, amount);
                }
            }

            logger.LogMessage("---------------------------------------------");

            this.logger.LogMessage("Done. Press any key to exit.");
        }
    }
}
