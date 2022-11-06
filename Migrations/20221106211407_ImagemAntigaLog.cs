using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astromedia.Migrations
{
    public partial class ImagemAntigaLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemAntiga",
                table: "LogsEdicoes",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemAntiga",
                table: "LogsEdicoes");
        }
    }
}
