using Microsoft.EntityFrameworkCore.Migrations;

namespace PoC.SharpDiff.Persistence.Migrations
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
                    LeftContentData = table.Column<string>(nullable: true),
                    RightContentData = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_Id",
                table: "Content",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Content_Id_LeftContentData",
                table: "Content",
                columns: new[] { "Id", "LeftContentData" });

            migrationBuilder.CreateIndex(
                name: "IX_Content_Id_RightContentData",
                table: "Content",
                columns: new[] { "Id", "RightContentData" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Content");
        }
    }
}
