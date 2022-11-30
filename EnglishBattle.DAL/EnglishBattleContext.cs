using EnglishBattle.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnglishBattle.DAL
{
    public class EnglishBattleContext : DbContext
    {
        public DbSet<Player> Players { get; protected set; }
        public DbSet<Game> Games { get; protected set; }
        public DbSet<GameAnswer> GameAnswers { get; protected set; }
        public DbSet<IrregularVerb> IrregularVerbs { get; protected set; }

        public EnglishBattleContext(DbContextOptions<EnglishBattleContext> options) : base(options)
        {

        }

        internal static string GetLocalDbPath()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            var dbPath = Path.Join(path, "englishBattle.sqlite");

            return dbPath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging();

            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //var dbPath = Path.Join(path, "englishBattle.db");

            //options.UseSqlite($"Data Source={dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Game>()
                .HasOne(x => x.Player)
                .WithMany(x => x.Games);

            builder.Entity<GameAnswer>()
                .HasKey(x => new { x.GameId, x.VerbId });

            builder.Entity<GameAnswer>()
                .HasOne(x => x.Game)
                .WithMany(x => x.Answers);

            builder.Entity<GameAnswer>()
                .HasOne(x => x.Verb)
                .WithMany();
        }
    }
}