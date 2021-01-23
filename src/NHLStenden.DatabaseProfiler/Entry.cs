using System;
using System.Data.SqlClient;

namespace NHLStenden.DatabaseProfiler
{
    // Icon for this application came from https://findicons.com/icon/62479/db_status
    // The icon is licensed under GNU/GPL

    // NOTE: this application will truncate the Series, Series_Genre and Genre tables for it's profiling.
    class Entry
    {
        static void Main(string[] args)
        {
            // I may be overengineering this a small bit, my apologies.
            var profiler = new Profiler();
            profiler.Start();
            Console.ReadKey();
        }
    }
}
