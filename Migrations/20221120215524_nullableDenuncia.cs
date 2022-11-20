using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Astromedia.Migrations
{
    public partial class nullableDenuncia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denuncias_Comentarios_ComentarioId",
                table: "Denuncias");

            migrationBuilder.DropForeignKey(
                name: "FK_Denuncias_Postagens_PostagemId",
                table: "Denuncias");

            migrationBuilder.AlterColumn<int>(
                name: "PostagemId",
                table: "Denuncias",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ComentarioId",
                table: "Denuncias",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncias_Comentarios_ComentarioId",
                table: "Denuncias",
                column: "ComentarioId",
                principalTable: "Comentarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncias_Postagens_PostagemId",
                table: "Denuncias",
                column: "PostagemId",
                principalTable: "Postagens",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Denuncias_Comentarios_ComentarioId",
                table: "Denuncias");

            migrationBuilder.DropForeignKey(
                name: "FK_Denuncias_Postagens_PostagemId",
                table: "Denuncias");

            migrationBuilder.AlterColumn<int>(
                name: "PostagemId",
                table: "Denuncias",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ComentarioId",
                table: "Denuncias",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncias_Comentarios_ComentarioId",
                table: "Denuncias",
                column: "ComentarioId",
                principalTable: "Comentarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Denuncias_Postagens_PostagemId",
                table: "Denuncias",
                column: "PostagemId",
                principalTable: "Postagens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
