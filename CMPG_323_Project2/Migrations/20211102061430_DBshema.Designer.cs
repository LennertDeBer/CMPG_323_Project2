// <auto-generated />
using System;
using CMPG_323_Project2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CMPG_323_Project2.Migrations
{
    [DbContext(typeof(CMPG_DBContext))]
    [Migration("20211102061430_DBshema")]
    partial class DBshema
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CMPG_323_Project2.Models.Album", b =>
                {
                    b.Property<int>("AlbumId")
                        .HasColumnType("int")
                        .HasColumnName("Album_ID");

                    b.Property<string>("AlbumName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Album_Name");

                    b.HasKey("AlbumId");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.AspNetUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "NormalizedEmail" }, "EmailIndex");

                    b.HasIndex(new[] { "NormalizedUserName" }, "UserNameIndex")
                        .IsUnique()
                        .HasFilter("([NormalizedUserName] IS NOT NULL)");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.AspNetUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserClaims_UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.AspNetUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex(new[] { "UserId" }, "IX_AspNetUserLogins_UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.Contain", b =>
                {
                    b.Property<int>("ContainId")
                        .HasColumnType("int")
                        .HasColumnName("Contain_ID");

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int")
                        .HasColumnName("Album_ID");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int")
                        .HasColumnName("Photo_ID");

                    b.HasKey("ContainId");

                    b.HasIndex(new[] { "AlbumId" }, "IX_Contain_Album_ID");

                    b.HasIndex(new[] { "PhotoId" }, "IX_Contain_Photo_ID");

                    b.ToTable("Contain");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.MetaDatum", b =>
                {
                    b.Property<int>("MetadataId")
                        .HasColumnType("int")
                        .HasColumnName("Metadata_ID");

                    b.Property<string>("CapturedBy")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Captured_By");

                    b.Property<DateTime?>("CapturedDate")
                        .HasColumnType("date")
                        .HasColumnName("Captured_Date");

                    b.Property<string>("Geolocation")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int")
                        .HasColumnName("Photo_ID");

                    b.Property<string>("Tags")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("MetadataId");

                    b.HasIndex(new[] { "PhotoId" }, "IX_MetaData_Photo_ID");

                    b.ToTable("MetaData");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.Photo", b =>
                {
                    b.Property<int>("PhotoId")
                        .HasColumnType("int")
                        .HasColumnName("Photo_ID");

                    b.Property<string>("PhotoUrl")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("Photo_URL");

                    b.HasKey("PhotoId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.ShareAlbum", b =>
                {
                    b.Property<int>("ShareAlbumId")
                        .HasColumnType("int")
                        .HasColumnName("Share_Album_ID");

                    b.Property<bool?>("AccessGranted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("Access_Granted")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("AlbumId")
                        .HasColumnType("int")
                        .HasColumnName("Album_ID");

                    b.Property<string>("RecipientUserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Recipient_User_ID");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("ShareAlbumId");

                    b.HasIndex(new[] { "AlbumId" }, "IX_Share_Album_Album_ID");

                    b.HasIndex(new[] { "RecipientUserId" }, "IX_Share_Album_Recipient_User_ID");

                    b.HasIndex(new[] { "UserId" }, "IX_Share_Album_User_ID");

                    b.ToTable("Share_Album");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.UserPhoto", b =>
                {
                    b.Property<int>("ShareId")
                        .HasColumnType("int")
                        .HasColumnName("Share_ID");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int")
                        .HasColumnName("Photo_ID");

                    b.Property<string>("RecepientUserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Recepient_User_ID");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("User_ID");

                    b.HasKey("ShareId");

                    b.HasIndex(new[] { "PhotoId" }, "IX_UserPhoto_Photo_ID");

                    b.HasIndex(new[] { "RecepientUserId" }, "IX_UserPhoto_Recepient_User_ID");

                    b.HasIndex(new[] { "UserId" }, "IX_UserPhoto_User_ID");

                    b.ToTable("UserPhoto");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.AspNetUserClaim", b =>
                {
                    b.HasOne("CMPG_323_Project2.Models.AspNetUser", "User")
                        .WithMany("AspNetUserClaims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.AspNetUserLogin", b =>
                {
                    b.HasOne("CMPG_323_Project2.Models.AspNetUser", "User")
                        .WithMany("AspNetUserLogins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.Contain", b =>
                {
                    b.HasOne("CMPG_323_Project2.Models.Album", "Album")
                        .WithMany("Contains")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("FK_Contain_Album")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMPG_323_Project2.Models.Photo", "Photo")
                        .WithMany("Contains")
                        .HasForeignKey("PhotoId")
                        .HasConstraintName("FK_Contain_Photo")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Album");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.MetaDatum", b =>
                {
                    b.HasOne("CMPG_323_Project2.Models.Photo", "Photo")
                        .WithMany("MetaData")
                        .HasForeignKey("PhotoId")
                        .HasConstraintName("FK_MetaData_Photo")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.ShareAlbum", b =>
                {
                    b.HasOne("CMPG_323_Project2.Models.Album", "Album")
                        .WithMany("ShareAlbums")
                        .HasForeignKey("AlbumId")
                        .HasConstraintName("FK_Share_Album_Album")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMPG_323_Project2.Models.AspNetUser", "RecipientUser")
                        .WithMany("ShareAlbumRecipientUsers")
                        .HasForeignKey("RecipientUserId")
                        .HasConstraintName("FK_Share_Album_AspNetUsers1");

                    b.HasOne("CMPG_323_Project2.Models.AspNetUser", "User")
                        .WithMany("ShareAlbumUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_Share_Album_AspNetUsers");

                    b.Navigation("Album");

                    b.Navigation("RecipientUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.UserPhoto", b =>
                {
                    b.HasOne("CMPG_323_Project2.Models.Photo", "Photo")
                        .WithMany("UserPhotos")
                        .HasForeignKey("PhotoId")
                        .HasConstraintName("FK_UserPhoto_Photo")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("CMPG_323_Project2.Models.AspNetUser", "RecepientUser")
                        .WithMany("UserPhotoRecepientUsers")
                        .HasForeignKey("RecepientUserId")
                        .HasConstraintName("FK_UserPhoto_AspNetUsers");

                    b.HasOne("CMPG_323_Project2.Models.AspNetUser", "User")
                        .WithMany("UserPhotoUsers")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserPhoto_UserPhoto");

                    b.Navigation("Photo");

                    b.Navigation("RecepientUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.Album", b =>
                {
                    b.Navigation("Contains");

                    b.Navigation("ShareAlbums");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.AspNetUser", b =>
                {
                    b.Navigation("AspNetUserClaims");

                    b.Navigation("AspNetUserLogins");

                    b.Navigation("ShareAlbumRecipientUsers");

                    b.Navigation("ShareAlbumUsers");

                    b.Navigation("UserPhotoRecepientUsers");

                    b.Navigation("UserPhotoUsers");
                });

            modelBuilder.Entity("CMPG_323_Project2.Models.Photo", b =>
                {
                    b.Navigation("Contains");

                    b.Navigation("MetaData");

                    b.Navigation("UserPhotos");
                });
#pragma warning restore 612, 618
        }
    }
}
