using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarGuidIdAbrigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abrigos_Usuario_UsuarioId",
                table: "Abrigos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Abrigos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "GuidId",
                table: "Abrigos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PessoasDesaparecidas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbrigoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NomeSearchable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: true),
                    InformacaoAdicional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasDesaparecidas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoasDesaparecidas_Abrigos_AbrigoId",
                        column: x => x.AbrigoId,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoasDesaparecidas_AbrigoId",
                table: "PessoasDesaparecidas",
                column: "AbrigoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Abrigos_Usuario_UsuarioId",
                table: "Abrigos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Abrigos_Usuario_UsuarioId",
                table: "Abrigos");

            migrationBuilder.DropTable(
                name: "PessoasDesaparecidas");

            migrationBuilder.DropColumn(
                name: "GuidId",
                table: "Abrigos");

            migrationBuilder.AlterColumn<Guid>(
                name: "UsuarioId",
                table: "Abrigos",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Abrigos_Usuario_UsuarioId",
                table: "Abrigos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }
    }
}
