using GameStore.Models;
using Microsoft.EntityFrameworkCore;
using GameStore.Data.Models;
using GameStore.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore
{
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public DbSet<Game> Games { get; set; }

        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartGame> ShoppingCartGames { get; set; }

        //public DbSet<UserWishList> UserWishListGames { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Platform> Platforms { get; set; }
 

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


            // modelBuilder.Entity<Article>()
            //.HasMany(c => c.Comments)
            //.WithOne(e => e.Article);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Article)
                .WithMany(c => c.Comments);
                //.HasForeignKey<Comment>(x => x.ArticleId);

        }

    }
}          
