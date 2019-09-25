using Microsoft.EntityFrameworkCore;
using System;
using System.IO;


namespace AlbumViewerBusiness
{
    public class AlbumViewerContext : DbContext
    {
        public string ConnectionString { get; set; } =
            "server=.;database=AlbumViewer4;integrated security=true;MultipleActiveResultSets=true;App=AlbumViewer";

        public bool useSqLite = true;

        public AlbumViewerContext(DbContextOptions options) : base(options)
        {         
            
        }

        public AlbumViewerContext()
        { }

        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Track> Tracks { get; set; }
        public DbSet<User> Users { get; set;  }

        
        protected override void OnModelCreating(ModelBuilder builder)
        {         
            base.OnModelCreating(builder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured)
                return;

            if (!useSqLite)
                optionsBuilder.UseSqlServer(ConnectionString);
            else
            {
                // Note this path has to have full  access for the Web user in order
                // to create the DB and write to it.
                var connStr = "Data Source=" +
                              Path.Combine(".\\AlbumViewerData.sqlite");
                optionsBuilder.UseSqlite(connStr);
            }
        }

    }
}