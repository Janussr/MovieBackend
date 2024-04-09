using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Movie.Api;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext()
    {
    }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DbContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Carts_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Carts).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasIndex(e => e.CartId, "IX_CartItems_CartId");

            entity.HasIndex(e => e.MovieId, "IX_CartItems_MovieId");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasForeignKey(d => d.CartId);

            entity.HasOne(d => d.Movie).WithMany(p => p.CartItems).HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.Property(e => e.Actors).HasMaxLength(255);
            entity.Property(e => e.Directors).HasMaxLength(255);
            entity.Property(e => e.Genre).HasMaxLength(255);
            entity.Property(e => e.LifetimeGrossInUsd)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("LifetimeGrossInUSD");
            entity.Property(e => e.Publishers).HasMaxLength(255);
            entity.Property(e => e.Runtime).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Orders_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasIndex(e => e.MovieId, "IX_OrderItems_MovieId");

            entity.HasIndex(e => e.OrderId, "IX_OrderItems_OrderId");

            entity.HasOne(d => d.Movie).WithMany(p => p.OrderItems).HasForeignKey(d => d.MovieId);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
