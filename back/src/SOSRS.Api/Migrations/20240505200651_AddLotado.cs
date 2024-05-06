using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddLotado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Lotado",
                table: "Abrigos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Lotado",
                table: "Abrigos");
        }
    }
}
