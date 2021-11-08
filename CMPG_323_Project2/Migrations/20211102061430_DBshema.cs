using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMPG_323_Project2.Migrations
{
    public partial class DBshema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Album",
                columns: table => new
                {
                    Album_ID = table.Column<int>(type: "int", nullable: false),
                    Album_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Album", x => x.Album_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Photo_ID = table.Column<int>(type: "int", nullable: false),
                    Photo_URL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Photo_ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Share_Album",
                columns: table => new
                {
                    Share_Album_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Album_ID = table.Column<int>(type: "int", nullable: true),
                    Recipient_User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Access_Granted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Share_Album", x => x.Share_Album_ID);
                    table.ForeignKey(
                        name: "FK_Share_Album_Album",
                        column: x => x.Album_ID,
                        principalTable: "Album",
                        principalColumn: "Album_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Share_Album_AspNetUsers",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Share_Album_AspNetUsers1",
                        column: x => x.Recipient_User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contain",
                columns: table => new
                {
                    Contain_ID = table.Column<int>(type: "int", nullable: false),
                    Album_ID = table.Column<int>(type: "int", nullable: true),
                    Photo_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contain", x => x.Contain_ID);
                    table.ForeignKey(
                        name: "FK_Contain_Album",
                        column: x => x.Album_ID,
                        principalTable: "Album",
                        principalColumn: "Album_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contain_Photo",
                        column: x => x.Photo_ID,
                        principalTable: "Photo",
                        principalColumn: "Photo_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetaData",
                columns: table => new
                {
                    Metadata_ID = table.Column<int>(type: "int", nullable: false),
                    Geolocation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Captured_Date = table.Column<DateTime>(type: "date", nullable: true),
                    Captured_By = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Photo_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaData", x => x.Metadata_ID);
                    table.ForeignKey(
                        name: "FK_MetaData_Photo",
                        column: x => x.Photo_ID,
                        principalTable: "Photo",
                        principalColumn: "Photo_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserPhoto",
                columns: table => new
                {
                    Share_ID = table.Column<int>(type: "int", nullable: false),
                    User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Photo_ID = table.Column<int>(type: "int", nullable: true),
                    Recepient_User_ID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPhoto", x => x.Share_ID);
                    table.ForeignKey(
                        name: "FK_UserPhoto_AspNetUsers",
                        column: x => x.Recepient_User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserPhoto_Photo",
                        column: x => x.Photo_ID,
                        principalTable: "Photo",
                        principalColumn: "Photo_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPhoto_UserPhoto",
                        column: x => x.User_ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Contain_Album_ID",
                table: "Contain",
                column: "Album_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Contain_Photo_ID",
                table: "Contain",
                column: "Photo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MetaData_Photo_ID",
                table: "MetaData",
                column: "Photo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Share_Album_Album_ID",
                table: "Share_Album",
                column: "Album_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Share_Album_Recipient_User_ID",
                table: "Share_Album",
                column: "Recipient_User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Share_Album_User_ID",
                table: "Share_Album",
                column: "User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoto_Photo_ID",
                table: "UserPhoto",
                column: "Photo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoto_Recepient_User_ID",
                table: "UserPhoto",
                column: "Recepient_User_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhoto_User_ID",
                table: "UserPhoto",
                column: "User_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "Contain");

            migrationBuilder.DropTable(
                name: "MetaData");

            migrationBuilder.DropTable(
                name: "Share_Album");

            migrationBuilder.DropTable(
                name: "UserPhoto");

            migrationBuilder.DropTable(
                name: "Album");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Photo");
        }
    }
}
