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

        public EfConnection(Logger logger)
        {
            this.logger = logger;
        }

        public void Connect()
        {
            connection = new NetflixContext();
            connection.Database.EnsureCreated();
            this.connected = true;
            this.logger.LogMessage($"Connected to an Entity Framework Core database.\n  - EF Provider: {connection.Database.ProviderName.Split('.').Last()}.", true);
        }

        public void Create(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                var series = connection.Series.Add(new Series()
                {
                    IsFilm = true,
                    AgeRestriction = 12,
                    Description = "Lorem Ipsum Doner Kebab",
                    Title = "Lorem Ipsum"
                }).Entity;

                var genre = connection.Genres.Add(new Genre()
                {
                    Name = "Creepy Movie :s"
                }).Entity;

                connection.SeriesGenres.Add(new SeriesGenre()
                {
                    SeriesId = series.Id,
                    GenreId = genre.Id
                });
            }

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
            slice.ForEachAsync(x => x.Title = "Lorem Ipsum Kebab");
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

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(Constants.EF_CONNECTION_STRING);

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
