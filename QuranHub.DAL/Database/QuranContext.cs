

namespace QuranHub.DAL.Database;

public class QuranContext : DbContext
{
    public  QuranContext(DbContextOptions<QuranContext> opts) : base(opts)
    { 
        this.Database.SetCommandTimeout(120);
    }

    public DbSet<Quran> Quran { get; set; }
    public DbSet<Muyassar> Muyassar { get; set; }
    public DbSet<IbnKatheer> IbnKatheer { get; set; }
    public DbSet<Tabary> Tabary { get; set; }
    public DbSet<Qortobi> Qortobi { get; set; }
    public DbSet<Jalalayn> Jalalayn { get; set; }
    public DbSet<Translation> Translation { get; set; }
    public DbSet<QuranClean> QuranClean { get; set; }
    public DbSet<MindMap> MindMaps { get; set; }

    // meta-data
    public DbSet<Sura> Suras { get; set; }
    public DbSet<Juz> Juzs { get; set; }
    public DbSet<Hizb>  Hizbs { get; set; }
    public DbSet<Manzil> Manzils { get; set; }
    public DbSet<Ruku> Rukus { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Sajda> Sajdas { get; set; }

    public DbSet<Section> Sections { get; set; }

    public DbSet<Hadith> Hadiths { get; set; }

    public DbSet<WeightVectorDimention> WeightVectorDimentions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}
