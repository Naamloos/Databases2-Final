using System;
using System.Data.SqlClient;
using System.IO;
using System.Text.Json;

namespace NHLStenden.DatabaseProfiler
{
    // Icon for this application came from https://findicons.com/icon/62479/db_status
    // The icon is licensed under GNU/GPL

    // NOTE: this application will truncate the Series, Series_Genre and Genre tables for it's profiling.
    class Entry
    {
        static void Main(string[] args)
        {
            Config cfg;
            if (!File.Exists("config.json"))
            {
                cfg = new Config();
                using(FileStream newfile = File.Create("config.json"))
                using(StreamWriter writer = new StreamWriter(newfile))
                {
                    writer.Write(JsonSerializer.Serialize(cfg));
                }

                Console.Write("Created a new config.json file with default values.\nPlease modify this file if needed, and rerun this program." +
                    $"\nFull path: {Path.GetFullPath("config.json")}\nPress any key to exit...");
                Console.ReadKey();
                return;
            }

            using (FileStream openfile = File.OpenRead("config.json"))
            using(StreamReader reader = new StreamReader(openfile))
            {
                cfg = JsonSerializer.Deserialize<Config>(reader.ReadToEnd());
            }

            // I may be overengineering this a small bit, my apologies.
            var profiler = new Profiler(cfg);
            profiler.Start();
            Console.ReadKey();
        }
    }
}
