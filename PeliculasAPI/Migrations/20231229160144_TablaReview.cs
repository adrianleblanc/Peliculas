using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PeliculasAPI.Migrations
{
    public partial class TablaReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasActores_Actores_ActorID",
                table: "PeliculasActores");

            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCine_Peliculas_PeliculaId",
                table: "PeliculasSalasDeCine");

            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCine_SalaDeCines_SalaDeCineID",
                table: "PeliculasSalasDeCine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaDeCines",
                table: "SalaDeCines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeliculasSalasDeCine",
                table: "PeliculasSalasDeCine");

            migrationBuilder.DropIndex(
                name: "IX_PeliculasSalasDeCine_PeliculaId",
                table: "PeliculasSalasDeCine");

            migrationBuilder.RenameTable(
                name: "SalaDeCines",
                newName: "SalasDeCine");

            migrationBuilder.RenameTable(
                name: "PeliculasSalasDeCine",
                newName: "PeliculasSalasDeCines");

            migrationBuilder.RenameColumn(
                name: "ActorID",
                table: "PeliculasActores",
                newName: "ActorId");

            migrationBuilder.RenameColumn(
                name: "nombre",
                table: "SalasDeCine",
                newName: "Nombre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalasDeCine",
                table: "SalasDeCine",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeliculasSalasDeCines",
                table: "PeliculasSalasDeCines",
                columns: new[] { "PeliculaId", "SalaDeCineID" });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Puntuacion = table.Column<int>(type: "int", nullable: false),
                    PeliculaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reviews_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Actores",
                columns: new[] { "Id", "FechaNacimiento", "Foto", "Nombre" },
                values: new object[,]
                {
                    { 5, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Jim Carrey" },
                    { 6, new DateTime(1965, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Robert Downey Jr." },
                    { 7, new DateTime(1981, 6, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Chris Evans" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d", "f8e2f8e3-4e43-4ea3-a29b-7b50ea24b930", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5673b8cf-12de-44f6-92ad-fae4a77932ad", 0, "27645e4c-3463-43ec-b58f-87b67ef4df14", "felipe@hotmail.com", false, false, null, "felipe@hotmail.com", "felipe@hotmail.com", "AQAAAAEAACcQAAAAEBTRPxPx3QP1QYS/NknCgTDk2ivGRmb+UHJYNbm7inFoKQLhrRDDZV8ZHL6M8tGZOw==", null, false, "97fe20f7-d478-41d2-a14a-ec601001dba3", false, "felipe@hotmail.com" });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 4, "Aventura" },
                    { 5, "Animación" },
                    { 6, "Suspenso" },
                    { 7, "Romance" }
                });

            migrationBuilder.InsertData(
                table: "Peliculas",
                columns: new[] { "Id", "EnCines", "FechaEstreno", "Poster", "Titulo" },
                values: new object[,]
                {
                    { 2, true, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Avengers: Endgame" },
                    { 3, false, new DateTime(2019, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Avengers: Infinity Wars" },
                    { 4, false, new DateTime(2020, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sonic the Hedgehog" },
                    { 5, false, new DateTime(2020, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Emma" },
                    { 6, false, new DateTime(2020, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Wonder Woman 1984" }
                });

            migrationBuilder.InsertData(
                table: "SalasDeCine",
                columns: new[] { "Id", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, "Agora", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.9388777 18.4839233)") },
                    { 4, "Sambil", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.9118804 18.4826214)") },
                    { 5, "Megacentro", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-69.856427 18.506934)") },
                    { 6, "Village East Cinema", (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-73.986227 40.730898)") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "5673b8cf-12de-44f6-92ad-fae4a77932ad" });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasDeCines_SalaDeCineID",
                table: "PeliculasSalasDeCines",
                column: "SalaDeCineID");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PeliculaId",
                table: "Reviews",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsuarioId",
                table: "Reviews",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasActores_Actores_ActorId",
                table: "PeliculasActores",
                column: "ActorId",
                principalTable: "Actores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCines_Peliculas_PeliculaId",
                table: "PeliculasSalasDeCines",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCines_SalasDeCine_SalaDeCineID",
                table: "PeliculasSalasDeCines",
                column: "SalaDeCineID",
                principalTable: "SalasDeCine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasActores_Actores_ActorId",
                table: "PeliculasActores");

            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCines_Peliculas_PeliculaId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCines_SalasDeCine_SalaDeCineID",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalasDeCine",
                table: "SalasDeCine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeliculasSalasDeCines",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropIndex(
                name: "IX_PeliculasSalasDeCines_SalaDeCineID",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Actores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Generos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Peliculas",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SalasDeCine",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5673b8cf-12de-44f6-92ad-fae4a77932ad");

            migrationBuilder.RenameTable(
                name: "SalasDeCine",
                newName: "SalaDeCines");

            migrationBuilder.RenameTable(
                name: "PeliculasSalasDeCines",
                newName: "PeliculasSalasDeCine");

            migrationBuilder.RenameColumn(
                name: "ActorId",
                table: "PeliculasActores",
                newName: "ActorID");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "SalaDeCines",
                newName: "nombre");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaDeCines",
                table: "SalaDeCines",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeliculasSalasDeCine",
                table: "PeliculasSalasDeCine",
                columns: new[] { "SalaDeCineID", "PeliculaId" });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasDeCine_PeliculaId",
                table: "PeliculasSalasDeCine",
                column: "PeliculaId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasActores_Actores_ActorID",
                table: "PeliculasActores",
                column: "ActorID",
                principalTable: "Actores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCine_Peliculas_PeliculaId",
                table: "PeliculasSalasDeCine",
                column: "PeliculaId",
                principalTable: "Peliculas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCine_SalaDeCines_SalaDeCineID",
                table: "PeliculasSalasDeCine",
                column: "SalaDeCineID",
                principalTable: "SalaDeCines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
