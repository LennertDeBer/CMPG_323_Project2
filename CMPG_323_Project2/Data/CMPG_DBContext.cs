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

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<Contain> Contains { get; set; }
        public virtual DbSet<MetaDatum> MetaData { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<ShareAlbum> ShareAlbums { get; set; }
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

            modelBuilder.Entity<Album>(entity =>
            {
                entity.Property(e => e.AlbumId).ValueGeneratedNever();
            });

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

            modelBuilder.Entity<Contain>(entity =>
            {
                entity.Property(e => e.ContainId).ValueGeneratedNever();

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.Contains)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Contain_Album");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Contains)
                    .HasForeignKey(d => d.PhotoId)
                    .HasConstraintName("FK_Contain_Photo");
            });

            modelBuilder.Entity<MetaDatum>(entity =>
            {
                entity.Property(e => e.MetadataId).ValueGeneratedNever();

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.MetaData)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_MetaData_Photo");
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.Property(e => e.PhotoId).ValueGeneratedNever();

                entity.Property(e => e.PhotoUrl).IsUnicode(false);
            });

            modelBuilder.Entity<ShareAlbum>(entity =>
            {
                entity.Property(e => e.ShareAlbumId).ValueGeneratedNever();

                entity.HasOne(d => d.Album)
                    .WithMany(p => p.ShareAlbums)
                    .HasForeignKey(d => d.AlbumId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Share_Album_Album");

                entity.HasOne(d => d.RecipientUser)
                    .WithMany(p => p.ShareAlbumRecipientUsers)
                    .HasForeignKey(d => d.RecipientUserId)
                    .HasConstraintName("FK_Share_Album_AspNetUsers1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShareAlbumUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Share_Album_AspNetUsers");
            });

            modelBuilder.Entity<UserPhoto>(entity =>
            {
                entity.Property(e => e.ShareId).ValueGeneratedNever();

                entity.Property(e => e.AccessGranted).IsFixedLength(true);

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.UserPhotos)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.Cascade)
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
