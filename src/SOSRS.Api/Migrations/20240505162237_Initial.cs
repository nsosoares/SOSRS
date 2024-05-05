using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SOSRS.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abrigos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NomeSearchable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    QuantidadeNecessariaVoluntarios = table.Column<int>(type: "int", nullable: true),
                    QuantidadeVagasDisponiveis = table.Column<int>(type: "int", nullable: true),
                    TipoChavePix = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    ChavePix = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    RuaSearchable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Numero = table.Column<int>(type: "int", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BairroSearchable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CidadeSearchable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    EstadoSearchable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abrigos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AbrigoId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NomeSearchable = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    QuantidadeNecessaria = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alimentos_Abrigos_AbrigoId",
                        column: x => x.AbrigoId,
                        principalTable: "Abrigos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alimentos_AbrigoId",
                table: "Alimentos",
                column: "AbrigoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alimentos");

            migrationBuilder.DropTable(
                name: "Abrigos");
        }
    }
}
