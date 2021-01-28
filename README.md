# Database Profiler
This is my Databases 2 End Assignment.

## Use case
For this profiler, I've decided to use the following three tables:
- `Series`
- `Genre`
- `SeriesGenre`

## Setup
### Requirements
- A working MSSQL Server.
- A working MongoDB Server.

### Step-by-step
1. Import the Netflix database from the `/dbs/` folder into MSSQL.
2. Create a database in your MongoDB.
3. Run the profiler once.
4. (**OPTIONAL**) Edit `Config.json` with your database values.
5. Run the profiler again. If all connections succeed, the setup was successful.

*Please do note that the profiler application expects empty databases to work. If you've ran the profiler before, I recommend resetting all databases if you want to run it once again.*

### Additional comments
- The connection strings can be modified in the generated `Config.json`.
- The created databases are `Netflix` and `NetflixCodeFirst` in SQL Server and `Netflix` in MongoDB.

## TODO
- Add MongoDB.