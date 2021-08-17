using GameStore.Data.Models;
using GameStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore
{

    public class ApplicationDbContext : IdentityDbContext<User>
    {
        
        public DbSet<Game> Games { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Platform> Platforms { get; set; }

        public DbSet<Cart> CartItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderGame> OrderGames { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(c => c.Comments);


            modelBuilder.Entity<OrderGame>()
               .HasKey(op => new { op.OrderId, op.GameId });

                modelBuilder.Entity<Order>()
               .HasMany(o => o.OrderGames)
               .WithOne(op => op.Order)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
               .HasKey(x => new { x.GameId, x.UserId });

        }

    }
}          
