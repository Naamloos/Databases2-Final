using Microsoft.EntityFrameworkCore;
using NHLStenden.DatabaseProfiler.DatabaseConnections.Abstraction;
using NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework.Entities;
using NHLStenden.DatabaseProfiler.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHLStenden.DatabaseProfiler.DatabaseConnections.EntityFramework
{
    public class EfConnection : IDatabaseConnection
    {
        private Logger logger;
        private NetflixContext connection;
        private bool connected = false;
        private Config config;

        public EfConnection(Logger logger, Config config)
        {
            this.logger = logger;
            this.config = config;
        }

        public void Connect()
        {
            connection = new NetflixContext(config);
            connection.Database.EnsureCreated();
            this.connected = true;
            this.logger.LogMessage($"Connected to an Entity Framework Core database.\n  - EF Provider: {connection.Database.ProviderName.Split('.').Last()}.", true);
        }

        public void Insert(int amount)
        {
            var series = new List<Series>();
            var genres = new List<Genre>();
            var seriesgenres = new List<SeriesGenre>();

            for (int i = 0; i < amount; i++)
            {
                var s = new Series()
                {
                    IsFilm = true,
                    AgeRestriction = 12,
                    Description = "Lorem Ipsum Doner Kebab",
                    Title = "Lorem Ipsum"
                };

                var g = new Genre()
                {
                    Name = "Creepy Movie :s"
                };

                series.Add(s);

                genres.Add(g);

                seriesgenres.Add(new SeriesGenre()
                {
                    Series = s,
                    Genre = g
                });
            }

            this.connection.Series.AddRange(series);
            this.connection.Genres.AddRange(genres);
            this.connection.SeriesGenres.AddRange(seriesgenres);

            connection.SaveChanges();
        }

        public void Delete(int amount)
        {
            // for EF core we'd need to SELECT first, then remove.
            // Deleting all table's entries.
            var slice = connection.SeriesGenres.Take(amount);
            connection.SeriesGenres.RemoveRange(slice);

            var slice2 = connection.Series.Take(amount);
            connection.Series.RemoveRange(slice2);

            var slice3 = connection.Genres.Take(amount);
            connection.Genres.RemoveRange(slice3);

            connection.SaveChanges();
        }

        public void Select(int amount)
        {
            var slice = connection.SeriesGenres.Take(amount);
        }

        public void Update(int amount)
        {
            var slice = connection.Series.Take(amount);
            foreach(var s in slice)
            {
                s.Title = "Lorem Ipsum Kebab";
            }
            connection.Series.UpdateRange(slice);
            connection.SaveChanges();
        }

        public string GetName() => $"EF Core ({connection.Database.ProviderName.Split('.').Last()})";

        public bool IsConnected() => this.connected;
    }

    class NetflixContext : DbContext
    {
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<PasswordReset> PasswordResets { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<SeriesGenre> SeriesGenres { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Subtitle> Subtitles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<View> Views { get; set; }
        public DbSet<WatchlistItem> WatchlistItems { get; set; }

        private string connectionstring { get; set; }

        public NetflixContext(Config config)
        {
            this.connectionstring = config.EntityFrameworkConnectionString;
            this.Database.SetCommandTimeout(TimeSpan.FromHours(1));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(this.connectionstring);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // overriding model creation for entities that
            // require +composite keys.
            modelBuilder.Entity<Episode>()
                .HasKey(e => new { e.EpisodeOrder, e.SeriesId });

            modelBuilder.Entity<WatchlistItem>()
                .HasKey(e => new { e.ProfileId, e.SeriesId });

            modelBuilder.Entity<View>()
                .HasKey(e => new { e.SeriesId, e.ProfileId });

            modelBuilder.Entity<SeriesGenre>()
                .HasKey(e => new { e.SeriesId, e.GenreId });

            // overriding model creation for entities that have a foreign key to it's own table
            modelBuilder.Entity<User>()
                .HasOne(e => e.InvitedBy)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
