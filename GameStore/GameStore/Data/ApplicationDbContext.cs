using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using GameStore.Data.Models;
using GameStore.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        
        public DbSet<Game> Games { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderGame> OrderGames { get; set; }

        public DbSet<UserInformation> UsersInformation { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(c => c.Comments);

            modelBuilder.Entity<UserInformation>()
               .HasOne(u => u.User)
               .WithOne(u => u.UserData)
               .HasForeignKey<UserInformation>(u => u.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderGame>()
               .HasKey(op => new { op.OrderId, op.GameId });

                modelBuilder.Entity<Order>()
               .HasMany(o => o.OrderGames)
               .WithOne(op => op.Order)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartItem>()
               .HasKey(x => new { x.GameId, x.UserId });

        }

    }
}          
