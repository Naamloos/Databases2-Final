using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction
{
    /// <summary>
    /// Database abstractions to simplify test creation.
    /// </summary>
    public interface IDatabaseConnection
    {
        /// <summary>
        /// Connects to the database.
        /// </summary>
        void Connect();

        /// <summary>
        /// Gets database type's name
        /// </summary>
        /// <returns>Database type name, e.g. "MongoDB"</returns>
        string GetName();

        /// <summary>
        /// Whether this connection is active.
        /// </summary>
        /// <returns>Connection active.</returns>
        bool IsConnected();

        void Insert(int amount);

        void Select(int amount);

        void Update(int amount);

        void Delete(int amount);
    }
}
