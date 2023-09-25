using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class GetterSetters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tamanho",
                columns: table => new
                {
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    QntdFatias = table.Column<int>(type: "INTEGER", nullable: false),
                    MultiplicadorPreco = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamanho", x => x.Nome);
                });

            migrationBuilder.CreateTable(
                name: "PizzaPedido",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Preco = table.Column<double>(type: "REAL", nullable: false),
                    TamanhoNome = table.Column<string>(type: "TEXT", nullable: false),
                    PedidoFinalId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PizzaPedido_PedidoFinal_PedidoFinalId",
                        column: x => x.PedidoFinalId,
                        principalTable: "PedidoFinal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PizzaPedido_Tamanho_TamanhoNome",
                        column: x => x.TamanhoNome,
                        principalTable: "Tamanho",
                        principalColumn: "Nome",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sabor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Preco = table.Column<double>(type: "REAL", nullable: false),
                    PizzaPedidoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sabor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sabor_PizzaPedido_PizzaPedidoId",
                        column: x => x.PizzaPedidoId,
                        principalTable: "PizzaPedido",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PizzaPedido_PedidoFinalId",
                table: "PizzaPedido",
                column: "PedidoFinalId");

            migrationBuilder.CreateIndex(
                name: "IX_PizzaPedido_TamanhoNome",
                table: "PizzaPedido",
                column: "TamanhoNome");

            migrationBuilder.CreateIndex(
                name: "IX_Sabor_PizzaPedidoId",
                table: "Sabor",
                column: "PizzaPedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sabor");

            migrationBuilder.DropTable(
                name: "PizzaPedido");

            migrationBuilder.DropTable(
                name: "Tamanho");
        }
    }
}
