using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astromedia.Migrations
{
    public partial class Relacionamentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LogsEdicoes_AstroId",
                table: "LogsEdicoes",
                column: "AstroId");

            migrationBuilder.CreateIndex(
                name: "IX_LogsEdicoes_PostagemId",
                table: "LogsEdicoes",
                column: "PostagemId");

            migrationBuilder.CreateIndex(
                name: "IX_LogsEdicoes_UsuarioId",
                table: "LogsEdicoes",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogsEdicoes_AspNetUsers_UsuarioId",
                table: "LogsEdicoes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LogsEdicoes_Astros_AstroId",
                table: "LogsEdicoes",
                column: "AstroId",
                principalTable: "Astros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LogsEdicoes_Postagens_PostagemId",
                table: "LogsEdicoes",
                column: "PostagemId",
                principalTable: "Postagens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogsEdicoes_AspNetUsers_UsuarioId",
                table: "LogsEdicoes");

            migrationBuilder.DropForeignKey(
                name: "FK_LogsEdicoes_Astros_AstroId",
                table: "LogsEdicoes");

            migrationBuilder.DropForeignKey(
                name: "FK_LogsEdicoes_Postagens_PostagemId",
                table: "LogsEdicoes");

            migrationBuilder.DropIndex(
                name: "IX_LogsEdicoes_AstroId",
                table: "LogsEdicoes");

            migrationBuilder.DropIndex(
                name: "IX_LogsEdicoes_PostagemId",
                table: "LogsEdicoes");

            migrationBuilder.DropIndex(
                name: "IX_LogsEdicoes_UsuarioId",
                table: "LogsEdicoes");
        }
    }
}
