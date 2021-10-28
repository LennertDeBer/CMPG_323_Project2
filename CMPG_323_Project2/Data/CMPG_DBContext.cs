using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using CMPG_323_Project2.Models;

#nullable disable

namespace CMPG_323_Project2.Data
{
    public partial class CMPG_DBContext : DbContext
    {
        public CMPG_DBContext()
        {
        }

        public CMPG_DBContext(DbContextOptions<CMPG_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<UserPhoto> UserPhotos { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=(local);Initial Catalog=CMPG_DB;Integrated Security=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.PhotoId).ValueGeneratedNever();

                entity.Property(e => e.PhotoUrl).IsUnicode(false);
            });

            modelBuilder.Entity<UserPhoto>(entity =>
            {
                entity.Property(e => e.ShareId).ValueGeneratedNever();

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.UserPhotos)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_UserPhoto_Photo");

                entity.HasOne(d => d.RecepientUser)
                    .WithMany(p => p.UserPhotoRecepientUsers)
                    .HasForeignKey(d => d.RecepientUserId)
                    .HasConstraintName("FK_UserPhoto_AspNetUsers");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserPhotoUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserPhoto_UserPhoto");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
