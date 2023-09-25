using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Estacionamento.Migrations
{
    /// <inheritdoc />
    public partial class CleintePedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClienteCpf",
                table: "PedidoFinal",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PedidoFinal_ClienteCpf",
                table: "PedidoFinal",
                column: "ClienteCpf");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoFinal_Cliente_ClienteCpf",
                table: "PedidoFinal",
                column: "ClienteCpf",
                principalTable: "Cliente",
                principalColumn: "Cpf");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoFinal_Cliente_ClienteCpf",
                table: "PedidoFinal");

            migrationBuilder.DropIndex(
                name: "IX_PedidoFinal_ClienteCpf",
                table: "PedidoFinal");

            migrationBuilder.DropColumn(
                name: "ClienteCpf",
                table: "PedidoFinal");
        }
    }
}
