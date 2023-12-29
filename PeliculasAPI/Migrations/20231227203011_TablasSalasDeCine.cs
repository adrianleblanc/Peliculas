﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasAPI.Migrations
{
    public partial class TablasSalasDeCine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalaDeCines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaDeCines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeliculasSalasDeCine",
                columns: table => new
                {
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    SalaDeCineID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculasSalasDeCine", x => new { x.SalaDeCineID, x.PeliculaId });
                    table.ForeignKey(
                        name: "FK_PeliculasSalasDeCine_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculasSalasDeCine_SalaDeCines_SalaDeCineID",
                        column: x => x.SalaDeCineID,
                        principalTable: "SalaDeCines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasDeCine_PeliculaId",
                table: "PeliculasSalasDeCine",
                column: "PeliculaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculasSalasDeCine");

            migrationBuilder.DropTable(
                name: "SalaDeCines");
        }
    }
}
