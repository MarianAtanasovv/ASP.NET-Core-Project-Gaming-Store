﻿using GamingWebAppDb.Models;
using Microsoft.EntityFrameworkCore;
using GamingWebAppDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingWebAppDb
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options)
        {

        }


        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<ShoppingCartGame> ShoppingCartGames { get; set; }

        public DbSet<UserWishList> UserWishListGames { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserWishList>()
                .HasKey(bc => new { bc.UserId, bc.GameId });

            modelBuilder.Entity<UserWishList>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.WishListGames)
                .HasForeignKey(bc => bc.GameId);

            modelBuilder.Entity<UserWishList>()
               .HasOne(bc => bc.Game)
               .WithMany(b => b.WishListGames)
               .HasForeignKey(bc => bc.UserId);

            modelBuilder.Entity<User>()
                 .HasOne<ShoppingCart>(x => x.ShoppingCart)
                 .WithOne(x => x.User)
                 .HasForeignKey<ShoppingCart>(x => x.ShoppingCartId);

        }
    }
}          
