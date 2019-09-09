using System;
using Diary.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Diary.Data.Context
{
    public sealed class ApplicationDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Entities.Diary> Diaries { get; set; }

        public ApplicationDatabaseContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            UsersConfig(modelBuilder);
            DiaryConfig(modelBuilder);
        }

        private void DiaryConfig(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Diary>(entity =>
            {
                entity.ToTable("diaries", "personal_diary");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();
                entity.Property(d => d.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValue(DateTime.UtcNow);
                entity.Property(d => d.Content)
                    .HasColumnName("content")
                    .HasColumnType("varchar(2500)")
                    .IsRequired();
                entity.Property(d => d.UserId)
                    .HasColumnName("user_id")
                    .HasColumnType("int(11)")
                    .IsRequired();
            });
        }


        private void UsersConfig(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("users", "personal_diary");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();
                entity.Property(u => u.CreatedAt)
                    .HasColumnName("created_at")
                    .HasColumnType("datetime")
                    .HasDefaultValue(DateTime.UtcNow);
                entity.Property(u => u.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(50)")
                    .IsRequired();
                entity.Property(u => u.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("varchar(40)")
                    .IsRequired();
                entity.Property(u => u.NickName)
                    .HasColumnName("nick_name")
                    .HasColumnType("varchar(40)")
                    .IsRequired()
                    .IsUnicode(true);
                entity.Property(u => u.Password)
                    .HasColumnName("password")
                    .HasColumnType("varchar(40)")
                    .IsRequired()
                    .IsUnicode();

                entity.
                    HasMany(u => u.Diaries).
                    WithOne(d => d.User).
                    IsRequired(true).
                    OnDelete(DeleteBehavior.Restrict).
                    HasForeignKey(d => d.UserId);
            });
        }

    }
   
}