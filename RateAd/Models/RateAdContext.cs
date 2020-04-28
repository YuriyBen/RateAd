using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RateAd.Models
{
    public partial class RateAdContext : DbContext
    {
        public RateAdContext()
        {
        }

        public RateAdContext(DbContextOptions<RateAdContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Gallery> Galleries { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=RateAd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories", "web");

                entity.HasIndex(e => e.Category1)
                    .HasName("UQ_Categories_Category")
                    .IsUnique();

                entity.Property(e => e.Category1)
                    .IsRequired()
                    .HasColumnName("Category")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comments", "web");

                entity.HasIndex(e => e.ImageId);

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasColumnName("Comment")
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK__Comments__image___40C49C62");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comments_UserId");
            });

            modelBuilder.Entity<Gallery>(entity =>
            {
                entity.ToTable("Gallery", "web");

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.Description)
                    .HasMaxLength(158)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Unknown description')");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Galleries)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Gallery__user_id__2AD55B43");
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Images", "web");

                entity.HasIndex(e => e.GalleryId);

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImagePath)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Categories_CategoryId");

                entity.HasOne(d => d.Gallery)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.GalleryId)
                    .HasConstraintName("FK_Images_GalleryId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Images__user_id__22401542");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles", "web");

                entity.Property(e => e.Description)
                    .HasMaxLength(158)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('Your permissions')");

                entity.Property(e => e.Role1)
                    .IsRequired()
                    .HasColumnName("Role")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Roles_UserId");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tags", "web");

                entity.HasIndex(e => e.ImageId);

                entity.HasIndex(e => e.Tag1);

                entity.Property(e => e.Tag1)
                    .IsRequired()
                    .HasColumnName("Tag")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Tags)
                    .HasForeignKey(d => d.ImageId)
                    .HasConstraintName("FK_Tags_ImageId");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users", "web");

                entity.HasIndex(e => e.Email)
                    .HasName("IX__Users__UniqueEmail")
                    .IsUnique();

                entity.HasIndex(e => e.PasswordRecoveryToken)
                    .HasName("IX__Users__PassRecoveryToken")
                    .IsUnique();

                entity.HasIndex(e => e.RegistrationToken)
                    .HasName("UQ_Users_RegistToken")
                    .IsUnique();

                entity.HasIndex(e => e.UserName)
                    .HasName("IX__Users__UniqueUserName")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordRecoveryToken)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
