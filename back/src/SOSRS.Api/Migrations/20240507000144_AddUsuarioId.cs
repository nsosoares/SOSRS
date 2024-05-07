using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodAcesso",
                table: "Logs");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Logs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "Abrigos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Abrigos");

            migrationBuilder.AddColumn<int>(
                name: "CodAcesso",
                table: "Logs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
