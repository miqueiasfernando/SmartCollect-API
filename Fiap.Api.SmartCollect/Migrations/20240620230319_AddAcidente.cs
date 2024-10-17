using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fiap.Api.Coletas.Migrations
{
    /// <inheritdoc />
    public partial class Addcoleta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_Coletas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    data_coleta = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    hora_coleta = table.Column<TimeSpan>(type: "INTERVAL DAY(8) TO SECOND(7)", nullable: false),
                    gravidade = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    endereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Coletas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_Coletas");
        }
    }
}
