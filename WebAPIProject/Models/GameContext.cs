using Microsoft.EntityFrameworkCore;

namespace WebAPIProject.Models
{
    public class GameContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Achievements> Achievements { get; set; }

       public GameContext(DbContextOptions opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
           
            model.Entity<User>().HasIndex(u => u.Email).IsUnique();
            model.Entity<GameGenre>().HasIndex(g => g.GenreName).IsUnique();
            model.Entity<Game>().HasIndex(g => g.GameName).IsUnique();
            model.Entity<Achievements>().HasIndex(a => a.AchievementName).IsUnique();
            model.Entity<User>().HasData(
                new User { UserId = 1, Name = "Sankar", Email = "sankar@gmail.com", Password = "1234567", Role = "Moderator" },
                new User { UserId= 2, Name = "Maheedhar", Email = "mahee@gmail.com", Password = "345781", Role = "Player" }
            );

            model.Entity<GameGenre>().HasData(
                new GameGenre { GameGenreId = "GEN01", GenreName = "Horror", Description = "Dark and terrifying survival horror experiences", Popularity = "High" },
                new GameGenre { GameGenreId = "GEN02", GenreName = "Stealth", Description = "Games focusing on avoiding detection", Popularity = "Medium" },
                new GameGenre { GameGenreId = "GEN03", GenreName = "Story-Driven", Description = "Narrative-rich deep plots", Popularity = "Very High" },
                new GameGenre { GameGenreId = "GEN04", GenreName = "Samurai", Description = "Japanese Samurai and Bushido-inspired action",Popularity = "High" }
            );

            model.Entity<Game>().HasData(
                new Game { GameId = "GAME01", GameName = "Resident Evil Village", Description = "Survival horror in a cursed village", Developer = "Capcom", ReleaseYear = "2021", GameGenreId = "GEN01" },
                new Game { GameId = "GAME02", GameName = "Silent Hill 2", Description = "Psychological horror classic", Developer = "Team Silent", ReleaseYear = "2001", GameGenreId = "GEN01" }
                
            );

            model.Entity<Achievements>().HasData(
                new Achievements { AchievementId = "ACH01", AchievementName = "Village Survivor", Badge = "Bronze", Difficulty = "Medium", Reward = "Ammo Pack", GameId = "GAME01" },
                new Achievements { AchievementId = "ACH02", AchievementName = "Monster Hunter", Badge = "Silver", Difficulty = "Hard", Reward = "Weapon Upgrade", GameId = "GAME01" }
               
            );
        }
    }
}

