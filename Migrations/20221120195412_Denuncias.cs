using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Astromedia.Migrations
{
    public partial class Denuncias : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Denuncias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<string>(type: "text", nullable: true),
                    PostagemId = table.Column<int>(type: "integer", nullable: true),
                    ComentarioId = table.Column<int>(type: "integer", nullable: true),
                    Conteudo = table.Column<string>(type: "text", nullable: true),
                    Respondida = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Denuncias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Denuncias_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Denuncias_Comentarios_ComentarioId",
                        column: x => x.ComentarioId,
                        principalTable: "Comentarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Denuncias_Postagens_PostagemId",
                        column: x => x.PostagemId,
                        principalTable: "Postagens",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Denuncias_ComentarioId",
                table: "Denuncias",
                column: "ComentarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Denuncias_PostagemId",
                table: "Denuncias",
                column: "PostagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Denuncias_UsuarioId",
                table: "Denuncias",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Denuncias");
        }
    }
}
