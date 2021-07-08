using Microsoft.EntityFrameworkCore.Migrations;

namespace SaeedNA.Web.Migrations
{
    public partial class OnlineUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnlineUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserGuid = table.Column<string>(nullable: true),
                    UserIp = table.Column<string>(nullable: true),
                    VisitDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineUsers", x => x.UserId);
                });

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 101,
                column: "SettingValue",
                value: "/uploads/img/SiteLogo.png");

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 102,
                column: "SettingValue",
                value: "/uploads/img/SiteLogo.png");

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 119,
                column: "SettingValue",
                value: "طراح سایت و اپلیکیشن");

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "SettingId", "SettingName", "SettingValue" },
                values: new object[,]
                {
                    { 133, "Age", "25" },
                    { 134, "Language", "فارسی، ترکی، انگلیسی" },
                    { 135, "Nationality", "ایران" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineUsers");

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 135);

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 101,
                column: "SettingValue",
                value: "/uploads/img/favicon.ico");

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 102,
                column: "SettingValue",
                value: "/uploads/img/favicon.ico");

            migrationBuilder.UpdateData(
                table: "SiteSettings",
                keyColumn: "SettingId",
                keyValue: 119,
                column: "SettingValue",
                value: "سایت شخصی سعید نوری");
        }
    }
}
