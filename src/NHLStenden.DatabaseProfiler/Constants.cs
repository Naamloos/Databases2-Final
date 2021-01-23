using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler
{
    /// <summary>
    /// This class contains a couple of constant values, e.g. connection string, database names, test amounts
    /// </summary>
    public static class Constants
    {
        public const string ADO_CONNECTION_STRING = "Integrated Security=SSPI;Initial Catalog=Netflix;Data Source=DESKTOP-HE189L1;";
        public const string EF_CONNECTION_STRING = "Integrated Security=SSPI;Initial Catalog=NetflixCodeFirst;Data Source=DESKTOP-HE189L1;";
        public const string MONGO_CONNECTION_STRING = "mongodb://127.0.0.1:27017";

        public const string MONGO_DATABASE_NAME = "netflix";
        public const string EF_DATABASE_NAME = "NetflixCodeFirst";
        public const string ADO_DATABASE_NAME = "Netflix";

        public static int[] AMOUNTS = { 1, 1000, 100000, 1000000 };
    }
}
