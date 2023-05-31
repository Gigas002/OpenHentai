using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;
using OpenHentai.Database.Tags;
using OpenHentai.Database.Creatures;
using OpenHentai.Database.Creations;
using OpenHentai.Database.Circles;
using OpenHentai.Descriptors;
using OpenHentai.Database.Relative;

namespace OpenHentai.Database;

public class DatabaseContext : DbContext
{
    #region Properties

    private readonly StreamWriter _logStream = new StreamWriter("log.txt", true);

    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Creation> Creations { get; set; } = null!;
    public DbSet<Manga> Mangas { get; set; } = null!;
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

    public DatabaseContext(string databasePath = "../openhentai.db") => DatabasePath = databasePath;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DatabasePath}")
                      .UseSnakeCaseNamingConvention()
                      .LogTo(_logStream.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // convertable properties

        modelBuilder.Entity<Tag>().Property(e => e.Description).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<IEnumerable<LanguageSpecificTextInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Creature>().Property(e => e.Description).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<IEnumerable<LanguageSpecificTextInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Creature>().Property(e => e.Media).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<List<MediaInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Author>().Property(e => e.ExternalLinks).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<List<ExternalLinkInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Creation>().Property(e => e.Sources).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<List<ExternalLinkInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Creation>().Property(e => e.Description).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<IEnumerable<LanguageSpecificTextInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Creation>().Property(e => e.Media).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<List<MediaInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Creation>().Property(e => e.Languages).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<List<LanguageInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Creation>().Property(e => e.Censorship).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<List<CensorshipInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        modelBuilder.Entity<Manga>().Property(e => e.ColoredInfo).HasConversion(
            v => JsonSerializer.Serialize(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }),
            v => JsonSerializer.Deserialize<List<ColoredInfo>>(v, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }));

        // modelBuilder.Entity<Creation>().UseTptMappingStrategy();

        // auto many-to-many zone

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Circles)
            .WithMany(c => c.Authors)
            .UsingEntity<Dictionary<string, object>>(
                "authors_circles",
                j => j
                    .HasOne<Circle>()
                    .WithMany()
                    .HasForeignKey("circle_id"),
                j => j
                    .HasOne<Author>()
                    .WithMany()
                    .HasForeignKey("author_id")
            );

        modelBuilder.Entity<Creation>()
            .HasMany(c => c.Circles)
            .WithMany(c => c.Creations)
            .UsingEntity<Dictionary<string, object>>(
                "creations_circles",
                j => j
                    .HasOne<Circle>()
                    .WithMany()
                    .HasForeignKey("circle_id"),
                j => j
                    .HasOne<Creation>()
                    .WithMany()
                    .HasForeignKey("creation_id")
            );

        modelBuilder.Entity<Creation>()
            .HasMany(c => c.Tags)
            .WithMany(t => t.Creations)
            .UsingEntity<Dictionary<string, object>>(
                "creations_tags",
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("tag_id"),
                j => j
                    .HasOne<Creation>()
                    .WithMany()
                    .HasForeignKey("creation_id")
            );

        modelBuilder.Entity<Creature>()
            .HasMany(c => c.Tags)
            .WithMany(t => t.Creatures)
            .UsingEntity<Dictionary<string, object>>(
                "creatures_tags",
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("tag_id"),
                j => j
                    .HasOne<Creature>()
                    .WithMany()
                    .HasForeignKey("creature_id")
            );

        // manual relations settings

        modelBuilder.Entity<CreationsCharacters>()
                    .HasOne(cc => cc.Creation)
                    .WithMany(c => c.Characters);

        modelBuilder.Entity<CreationsCharacters>()
                    .HasOne(cc => cc.Character)
                    .WithMany(c => c.InCreations);

        modelBuilder.Entity<CreaturesRelations>()
                    .HasOne(cr => cr.Creature)
                    .WithMany(c => c.Relations);

        modelBuilder.Entity<CreaturesRelations>()
                    .HasOne(cr => cr.RelatedCreature);

        modelBuilder.Entity<AuthorsCreations>()
                    .HasOne(ac => ac.Author)
                    .WithMany(a => a.Creations);

        modelBuilder.Entity<AuthorsCreations>()
                    .HasOne(ac => ac.Creation)
                    .WithMany(c => c.Authors);

        modelBuilder.Entity<CreationsRelations>()
                    .HasOne(cr => cr.Creation)
                    .WithMany(c => c.Relations);

        modelBuilder.Entity<CreationsRelations>()
                    .HasOne(cr => cr.RelatedCreation);
    }

    public override void Dispose()
    {
        base.Dispose();
        _logStream.Dispose();
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await _logStream.DisposeAsync();
    }
}
