using Microsoft.EntityFrameworkCore;
using OpenHentai.Tags;
using OpenHentai.Creatures;
using OpenHentai.Creations;
using OpenHentai.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Relative;
using OpenHentai.ValueConverters;
using OpenHentai.Constants;

namespace OpenHentai;

public class DatabaseContext : DbContext
{
    #region Constants

    // for debug purposes
    internal const string LogPath = "../log.txt";
    internal const string SqliteDatabasePath = "../openhentai.db";

    #endregion

    #region Properties

    private readonly StreamWriter _logStream = new(LogPath, true);

    public DbSet<Tag> Tags { get; set; } = null!;
    
    public DbSet<Creation> Creations { get; set; } = null!;
    
    public DbSet<Manga> Manga { get; set; } = null!;
    
    public DbSet<Creature> Creatures { get; set; } = null!;
    
    public DbSet<CreaturesNames> CreaturesNames { get; set; } = null!;
    
    public DbSet<Author> Authors { get; set; } = null!;
    
    public DbSet<Character> Characters { get; set; } = null!;
    
    public DbSet<CreationsCharacters> CreationsCharacters { get; set; } = null!;
    
    public DbSet<Circle> Circles { get; set; } = null!;
    
    public DbSet<CreaturesRelations> CreaturesRelations { get; set; } = null!;
    
    public DbSet<AuthorsNames> AuthorsNames { get; set; } = null!;
    
    public DbSet<AuthorsCreations> AuthorsCreations { get; set; } = null!;
    
    public DbSet<CirclesTitles> CirclesTitles { get; set; } = null!;
    
    public DbSet<CreationsTitles> CreationsTitles { get; set; } = null!;
    
    public DbSet<CreationsRelations> CreationsRelations { get; set; } = null!;

    public string DatabasePath { get; init; } = null!;

    #endregion

    public DatabaseContext(string databasePath = SqliteDatabasePath) => DatabasePath = databasePath;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}")
                      .UseSnakeCaseNamingConvention()
                      .LogTo(_logStream.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null) throw new ArgumentNullException(nameof(modelBuilder));

        #region Force strategy

        // table-per-hierarchy; parent and children in one table
        // modelBuilder.Entity<Creation>().UseTphMappingStrategy();

        // table-per-type: every type has own table
        // modelBuilder.Entity<Creation>().UseTptMappingStrategy();
        // modelBuilder.Entity<Creature>().UseTptMappingStrategy();

        // table-per-class: every class has own table
        // modelBuilder.Entity<Creation>().UseTptMappingStrategy();

        #endregion

        #region Convertable properties

        var jsonSerializerOptions = Essential.JsonSerializerOptions;
        
        // TODO: probably redundant in net8+
        // see: https://github.com/dotnet/efcore/issues/13947

        modelBuilder.Entity<Tag>().Property(e => e.Description)
                                  .HasConversion<JsonValueConverter<HashSet<LanguageSpecificTextInfo>>>();

        modelBuilder.Entity<Creature>().Property(e => e.Description)
                                  .HasConversion<JsonValueConverter<HashSet<LanguageSpecificTextInfo>>>();

        modelBuilder.Entity<Creature>().Property(e => e.Media)
                                  .HasConversion<JsonValueConverter<HashSet<MediaInfo>>>();        

        modelBuilder.Entity<Author>().Property(e => e.ExternalLinks)
                                  .HasConversion<JsonValueConverter<HashSet<ExternalLinkInfo>>>();        

        modelBuilder.Entity<Creation>().Property(e => e.Sources)
                                  .HasConversion<JsonValueConverter<HashSet<ExternalLinkInfo>>>();        

        modelBuilder.Entity<Creation>().Property(e => e.Description)
                                  .HasConversion<JsonValueConverter<HashSet<LanguageSpecificTextInfo>>>();        

        modelBuilder.Entity<Creation>().Property(e => e.Media)
                                  .HasConversion<JsonValueConverter<HashSet<MediaInfo>>>();        

        modelBuilder.Entity<Creation>().Property(e => e.Languages)
                                  .HasConversion<JsonValueConverter<HashSet<LanguageInfo>>>();        

        modelBuilder.Entity<Creation>().Property(e => e.Censorship)
                                  .HasConversion<JsonValueConverter<HashSet<CensorshipInfo>>>();        

        modelBuilder.Entity<Manga>().Property(e => e.ColoredInfo)
                                  .HasConversion<JsonValueConverter<HashSet<ColoredInfo>>>();        

        #endregion

        #region Auto many-to-many zone

        // see: https://github.com/dotnet/efcore/issues/31019

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Circles)
            .WithMany(c => c.Authors)
            .UsingEntity<Dictionary<ulong, ulong>>(
                TableNames.AuthorsCircles,
                j => j
                    .HasOne<Circle>()
                    .WithMany()
                    .HasForeignKey(FieldNames.CircleId),
                j => j
                    .HasOne<Author>()
                    .WithMany()
                    .HasForeignKey(FieldNames.AuthorId)
            );

        modelBuilder.Entity<Creation>()
            .HasMany(c => c.Circles)
            .WithMany(c => c.Creations)
            .UsingEntity<Dictionary<ulong, ulong>>(
                TableNames.CreationsCircles,
                j => j
                    .HasOne<Circle>()
                    .WithMany()
                    .HasForeignKey(FieldNames.CircleId),
                j => j
                    .HasOne<Creation>()
                    .WithMany()
                    .HasForeignKey(FieldNames.CreationId)
            );

        modelBuilder.Entity<Creation>()
            .HasMany(c => c.Tags)
            .WithMany(t => t.Creations)
            .UsingEntity<Dictionary<ulong, ulong>>(
                TableNames.CreationsTags,
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey(FieldNames.TagId),
                j => j
                    .HasOne<Creation>()
                    .WithMany()
                    .HasForeignKey(FieldNames.CreationId)
            );

        modelBuilder.Entity<Creature>()
            .HasMany(c => c.Tags)
            .WithMany(t => t.Creatures)
            .UsingEntity<Dictionary<ulong, ulong>>(
                TableNames.CreaturesTags,
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey(FieldNames.TagId),
                j => j
                    .HasOne<Creature>()
                    .WithMany()
                    .HasForeignKey(FieldNames.CreatureId)
            );

        modelBuilder.Entity<Circle>()
            .HasMany(c => c.Tags)
            .WithMany(t => t.Circles)
            .UsingEntity<Dictionary<ulong, ulong>>(
                TableNames.CirclesTags,
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey(FieldNames.TagId),
                j => j
                    .HasOne<Circle>()
                    .WithMany()
                    .HasForeignKey(FieldNames.CircleId)
            );

        #endregion

        #region Manual relations settings

        modelBuilder.Entity<CreaturesRelations>()
                    .HasOne(cr => cr.Origin)
                    .WithMany(c => c.CreaturesRelations);

        modelBuilder.Entity<CreationsRelations>()
                    .HasOne(cr => cr.Origin)
                    .WithMany(c => c.CreationsRelations);

        #endregion
    }

    public override void Dispose()
    {
        base.Dispose();
        _logStream.Dispose();
        GC.SuppressFinalize(this);
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync().ConfigureAwait(false);
        await _logStream.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }
}
