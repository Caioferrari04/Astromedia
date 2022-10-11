using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astromedia.Migrations
{
    public partial class AtributoImagemPostagem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagem",
                table: "Postagens",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Postagens");
        }
    }
}
