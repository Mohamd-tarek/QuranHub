using Microsoft.EntityFrameworkCore;

namespace ServerApp.Models
{
    public class DataContext : DbContext
    {
        public  DataContext(DbContextOptions<DataContext> opts)
            : base(opts) { }

        public DbSet<Quran> Quran { get; set; }
        public DbSet<Tafseer> Tafseer { get; set; }
        public DbSet<Translation> Translation { get; set; }
        public DbSet<QuranClean> QuranClean { get; set; }
        public DbSet<Note> Note { get; set; }

        // meta-data
        public DbSet<Sura> Suras { get; set; }
        public DbSet<Juz> Juzs { get; set; }
        public DbSet<Hizb>  Hizbs { get; set; }
        public DbSet<Manzil> Manzils { get; set; }
        public DbSet<Ruku> Rukus { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<Sajda> Sajdas { get; set; }

    }
}
