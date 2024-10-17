using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.Coletas.Migrations
{
    /// <inheritdoc />
    public partial class AddSmartCollect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_Coletas",
                table: "tb_Coletas");

            migrationBuilder.RenameTable(
                name: "tb_Coletas",
                newName: "tb_coletas");

            migrationBuilder.RenameColumn(
                name: "gravidade",
                table: "tb_coletas",
                newName: "tipo_residuo");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_coletas",
                table: "tb_coletas",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_coletas",
                table: "tb_coletas");

            migrationBuilder.RenameTable(
                name: "tb_coletas",
                newName: "tb_Coletas");

            migrationBuilder.RenameColumn(
                name: "tipo_residuo",
                table: "tb_Coletas",
                newName: "gravidade");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_Coletas",
                table: "tb_Coletas",
                column: "Id");
        }
    }
}
