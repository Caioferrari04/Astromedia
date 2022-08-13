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
                name: "AstroId",
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
                    Curiosidades = table.Column<string>(type: "text", nullable: true),
                    Foto = table.Column<string>(type: "text", nullable: true)
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
                    UsuariosId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AstroUsuario", x => new { x.AstrosId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_AstroUsuario_AspNetUsers_UsuariosId",
                        column: x => x.UsuariosId,
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
                name: "IX_Postagens_AstroId",
                table: "Postagens",
                column: "AstroId");

            migrationBuilder.CreateIndex(
                name: "IX_AstroUsuario_UsuariosId",
                table: "AstroUsuario",
                column: "UsuariosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Postagens_Astros_AstroId",
                table: "Postagens",
                column: "AstroId",
                principalTable: "Astros",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Postagens_Astros_AstroId",
                table: "Postagens");

            migrationBuilder.DropTable(
                name: "AstroUsuario");

            migrationBuilder.DropTable(
                name: "Astros");

            migrationBuilder.DropIndex(
                name: "IX_Postagens_AstroId",
                table: "Postagens");

            migrationBuilder.DropColumn(
                name: "AstroId",
                table: "Postagens");
        }
    }
}
