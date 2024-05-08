using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class CriarFlagTipoAbrigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AbrigoDeAnimais",
                table: "Abrigos",
                newName: "PermiteAnimais");

            migrationBuilder.AddColumn<byte>(
                name: "TipoAbrigo",
                table: "Abrigos",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoAbrigo",
                table: "Abrigos");

            migrationBuilder.RenameColumn(
                name: "PermiteAnimais",
                table: "Abrigos",
                newName: "AbrigoDeAnimais");
        }
    }
}
