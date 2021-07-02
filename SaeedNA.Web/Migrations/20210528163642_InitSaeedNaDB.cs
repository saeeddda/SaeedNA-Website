using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaeedNA.Web.Migrations
{
    public partial class InitSaeedNaDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(maxLength: 40, nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailName = table.Column<string>(nullable: true),
                    EmailPhone = table.Column<string>(nullable: true),
                    EmailEmail = table.Column<string>(nullable: true),
                    EmailText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    HistoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryTitle = table.Column<string>(maxLength: 30, nullable: false),
                    HistoryWorkPlace = table.Column<string>(maxLength: 30, nullable: false),
                    HistoryDate = table.Column<string>(maxLength: 30, nullable: false),
                    HistoryDescription = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.HistoryId);
                });

            migrationBuilder.CreateTable(
                name: "MyServices",
                columns: table => new
                {
                    MyServiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyServiceTitle = table.Column<string>(maxLength: 30, nullable: false),
                    MyServiceText = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyServices", x => x.MyServiceId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCounters",
                columns: table => new
                {
                    ServiceCounterId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCounterTitle = table.Column<string>(maxLength: 30, nullable: false),
                    ServiceCounterCount = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCounters", x => x.ServiceCounterId);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    SettingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingName = table.Column<string>(nullable: true),
                    SettingValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.SettingId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillTitle = table.Column<string>(maxLength: 50, nullable: false),
                    SkillProgress = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
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
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTitle = table.Column<string>(maxLength: 64, nullable: false),
                    PostCreateDate = table.Column<string>(nullable: true),
                    PostShortDescription = table.Column<string>(maxLength: 150, nullable: true),
                    PostDescription = table.Column<string>(nullable: true),
                    PostTags = table.Column<string>(nullable: true),
                    ProjectCustomer = table.Column<string>(nullable: true),
                    ProjectAddress = table.Column<string>(nullable: true),
                    ProjectLanguage = table.Column<string>(nullable: true),
                    PostType = table.Column<string>(nullable: true),
                    IsPublished = table.Column<bool>(nullable: false),
                    PostVisit = table.Column<int>(nullable: false),
                    PostFile = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostId);
                    table.ForeignKey(
                        name: "FK_Posts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MyServices",
                columns: new[] { "MyServiceId", "MyServiceText", "MyServiceTitle" },
                values: new object[,]
                {
                    { 101, "طراحی سایت طبق نیاز شما", "طراحی وب" },
                    { 102, "طراحی اپلیکیشن موبایل برای انواع نیاز ها", "طراحی اپلیکیشن" },
                    { 103, "طراحی انواع کارت ویزیت و تراکت", "طراحی گرافیکی" },
                    { 104, "انجام پشتیبانی بر روی پروژه های شما", "پشتیبانی" }
                });

            migrationBuilder.InsertData(
                table: "ServiceCounters",
                columns: new[] { "ServiceCounterId", "ServiceCounterCount", "ServiceCounterTitle" },
                values: new object[,]
                {
                    { 101, "80", "انجام شده" },
                    { 102, "850", "فنجان قهوه" },
                    { 103, "56", "مشتریان" },
                    { 104, "4000", "ساعت کد زدن" }
                });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "SettingId", "SettingName", "SettingValue" },
                values: new object[,]
                {
                    { 115, "FullName", "سعید نوری" },
                    { 116, "Birthday", "1375/04/20" },
                    { 117, "Mobile", "09101650281" },
                    { 118, "AboutMe", "سایت شخصی سعید نوری" },
                    { 119, "Slogans", "سایت شخصی سعید نوری" },
                    { 120, "ResumeImage", "/uploads/img/2.jpg" },
                    { 121, "AvatarImage", "/uploads/img/img-mobile.jpg" },
                    { 125, "Email", "i@saeedna.ir" },
                    { 123, "Address", "ایران، تهران" },
                    { 124, "PhoneNumber", "09101650281" },
                    { 114, "AboutMeMenu", "true" },
                    { 126, "Telegram", "https://t.me/saeeddda_main" },
                    { 127, "Instagram", "https://instagram.com/saeeddda_main" },
                    { 128, "Twitter", "https://twitter.com/saeeddda" },
                    { 129, "Facebook", "#" },
                    { 122, "ResumeFile", "/uploads/img/resume.pdf" },
                    { 113, "ContactMeMenu", "true" },
                    { 109, "GoogleAnalytics", "" },
                    { 111, "PortfolioMenu", "true" },
                    { 101, "SiteLogo", "/uploads/img/favicon.ico" },
                    { 112, "BlogMenu", "true" },
                    { 103, "SiteUrl", "https://saeedna.ir/" },
                    { 104, "SiteTitle", "سایت شخصی سعید نوری" },
                    { 105, "SiteMode", "dark" },
                    { 102, "SiteFavIcon", "/uploads/img/favicon.ico" },
                    { 107, "MetaTags", "شخصی,سایت,رزومه,وردپرس,نوری,wordpress,طراحی" },
                    { 108, "MetaDescription", "سایت شخصی سعید نوری. انجام طراحی سایت و گرافیک" },
                    { 130, "Youtube", "#" },
                    { 110, "MainMenu", "true" },
                    { 106, "SiteColor", "blue" },
                    { 131, "Linkedin", "#" }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "SkillProgress", "SkillTitle" },
                values: new object[,]
                {
                    { 106, "60", "Seo" },
                    { 105, "80", "PhotoShop" },
                    { 104, "90", "HTML, Css" },
                    { 103, "85", "Wordpress" },
                    { 102, "60", "ASP.Net Core" },
                    { 101, "80", "C#" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CategoryId",
                table: "Posts",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropTable(
                name: "MyServices");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "ServiceCounters");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
