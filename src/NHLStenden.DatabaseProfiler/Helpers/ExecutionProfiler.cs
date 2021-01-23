using NHLStenden.DatabaseProfiler.Attributes;
using NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.Helpers
{
    public class ExecutionProfiler
    {
        public delegate void ProfilableMethod(IDatabaseConnection database, int amount);

        private Logger logger;
        private Stopwatch stopwatch;
        private List<IDatabaseConnection> databases;

        public ExecutionProfiler(Logger logger)
        {
            this.logger = logger;
            this.stopwatch = new Stopwatch();
            this.databases = new List<IDatabaseConnection>();
        }

        public void RegisterDatabase(IDatabaseConnection database)
        {
            if (this.databases.Contains(database))
                throw new Exception("This database was already registered!");
            if (!database.IsConnected())
                throw new Exception("Please ensure the database is connected before registering it to the execution profiler!");

            this.databases.Add(database);
        }

        public void ProfileExecution(ProfilableMethod method, int amount)
        {
            var att = method.Method.GetCustomAttributes(false);
            if (!att.Any(x => x.GetType() == typeof(ProfilerInfoAttribute)))
            {
                throw new InvalidOperationException("A delegate passed as a parameter to the ProfileExecution method has to be derived from a method that has the ProfilerInfo attribute!");
            }

            logger.LogMessage("---------------------------------------------");
            var info = (ProfilerInfoAttribute)method.Method.GetCustomAttributes(false).FirstOrDefault();
            logger.LogMessage($"Method Name: '{info.Name}'\nMethod Description: '{info.Description}'\nAmount: {amount}", true);

            Dictionary<IDatabaseConnection, long> timings = new Dictionary<IDatabaseConnection, long>();

            foreach (var db in this.databases)
            {
                stopwatch.Reset();
                stopwatch.Start();
                method(db, amount);
                stopwatch.Stop();
                logger.LogMessage($"{db.GetName().PadRight(20)}: {stopwatch.ElapsedMilliseconds} ms.");
                timings.Add(db, stopwatch.ElapsedMilliseconds);
            }

            var lowest = timings.Values.Min();
            logger.LogMessage($"Fastest: {string.Join(", ", timings.Where(x => x.Value == lowest).Select(x => $"'{x.Key.GetName()}'"))} ({lowest} ms).", true);
        }
    }
}
