using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Astromedia.Migrations
{
    public partial class Astro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "astroId",
                table: "Postagens",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Astros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Curiosidades = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Astros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AstroUsuario",
                columns: table => new
                {
                    AstrosId = table.Column<int>(type: "integer", nullable: false),
                    usuariosId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstroUsuario", x => new { x.AstrosId, x.usuariosId });
                    table.ForeignKey(
                        name: "FK_AstroUsuario_AspNetUsers_usuariosId",
                        column: x => x.usuariosId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AstroUsuario_Astros_AstrosId",
                        column: x => x.AstrosId,
                        principalTable: "Astros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Postagens_astroId",
                table: "Postagens",
                column: "astroId");

            migrationBuilder.CreateIndex(
                name: "IX_AstroUsuario_usuariosId",
                table: "AstroUsuario",
                column: "usuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Astros_astroId",
                table: "Postagens",
                column: "astroId",
                principalTable: "Astros",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Astros_astroId",
                table: "Postagens");

            migrationBuilder.DropTable(
                name: "AstroUsuario");

            migrationBuilder.DropTable(
                name: "Astros");

            migrationBuilder.DropIndex(
                name: "IX_Postagens_astroId",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "astroId",
                table: "Postagens");
        }
    }
}
