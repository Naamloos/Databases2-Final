using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TODO deserialize from JSON file if I have time left
namespace NHLStenden.DatabaseProfiler
{
    /// <summary>
    /// This class contains a couple of constant values, e.g. connection string, database names, test amounts
    /// </summary>
    public class Config
    {
        public string AdoNetConnectionString = "Integrated Security=SSPI;Initial Catalog=Netflix;Data Source=DESKTOP-HE189L1;";
        public string EntityFrameworkConnectionString = "Integrated Security=SSPI;Initial Catalog=NetflixCodeFirst;Data Source=DESKTOP-HE189L1;";
        public string MongoConnectionString = "mongodb://127.0.0.1:27017";

        public string MongoDatabaseName = "Netflix";
        public string EntityFrameworkDatabaseName = "NetflixCodeFirst";
        public string AdoNetDatabaseName = "Netflix";

        public int[] amounts = { 1, 1000, 100000, 1000000 };
    }
}
