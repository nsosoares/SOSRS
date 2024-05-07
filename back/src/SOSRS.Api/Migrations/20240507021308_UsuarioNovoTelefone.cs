using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioNovoTelefone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Telefone",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telefone",
                table: "Usuario");
        }
    }
}
