using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddObsAbrigo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Abrigos",
                type: "nvarchar(800)",
                maxLength: 800,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Abrigos");
        }
    }
}
