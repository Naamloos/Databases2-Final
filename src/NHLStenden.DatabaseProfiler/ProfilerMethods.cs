using NHLStenden.DatabaseProfiler.Attributes;
using NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler
{
    /// <summary>
    /// This class contains all methods for the profiler to execute.
    /// </summary>
    public static class ProfilerMethods
    {
        // NOTE TO SELF: I am { { quite } } sure reflection will get these methods in order.
        // if not, maybe add an integer to the ProfilerAttribute for 
        // execution order and order with linq in Profiler.cs?

        // Profiler methods have to be public + static else I can not find them (easily) with reflection.
        [ProfilerInfo("Insert", "Profiler test for INSERT queries.")]
        public static void InsertMethod(IDatabaseConnection database, int amount)
        {
            database.Insert(amount);
        }

        [ProfilerInfo("Select", "Profiler test for SELECT queries.")]
        public static void SelectMethod(IDatabaseConnection database, int amount)
        {
            database.Select(amount);
        }

        [ProfilerInfo("Update", "Profiler test for UPDATE queries.")]
        public static void UpdateMethod(IDatabaseConnection database, int amount)
        {
            database.Update(amount);
        }

        [ProfilerInfo("Delete", "Profiler test for DELETE queries.")]
        public static void DeleteMethod(IDatabaseConnection database, int amount)
        {
            database.Delete(amount);
        }
    }
}
