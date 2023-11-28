using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConversionWebMVC.Migrations
{
    public partial class inicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Divisa",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    acronimo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nombre_divisa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<int>(type: "int", nullable: false),
                    nombre_pais = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisa", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Historico",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tiempo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValorInicial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorFinal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    nombre_pais = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Divisa");

            migrationBuilder.DropTable(
                name: "Historico");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
