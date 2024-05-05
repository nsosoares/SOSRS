using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddComplemtentoCapTotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacidadeTotalPessoas",
                table: "Abrigos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Abrigos",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacidadeTotalPessoas",
                table: "Abrigos");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Abrigos");
        }
    }
}
