using Microsoft.EntityFrameworkCore.Migrations;

namespace PoC.SharpDiff.WebAPI.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Direction = table.Column<int>(nullable: false),
                    Base64String = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => new { x.Id, x.Direction });
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_Id_Direction",
                table: "Content",
                columns: new[] { "Id", "Direction" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");
        }
    }
}
