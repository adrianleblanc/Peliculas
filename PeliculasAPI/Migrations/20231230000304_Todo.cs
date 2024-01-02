using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PeliculasAPI.Migrations
{
    public partial class Todo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d",
                column: "ConcurrencyStamp",
                value: "8c8338f2-f581-43ce-ad81-b72767918e0a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5673b8cf-12de-44f6-92ad-fae4a77932ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5e68ad43-3dfc-43b8-b807-12af21dcec9a", "AQAAAAEAACcQAAAAEF1IACv45uvABVEb0OxTjUllBhVa5NWAQVATbBf68h4E5oJKrNnCIKccndBnmz/fxw==", "638a0293-e990-4bdd-a667-3c0789e02dc1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aae0b6d-d50c-4d0a-9b90-2a6873e3845d",
                column: "ConcurrencyStamp",
                value: "f8e2f8e3-4e43-4ea3-a29b-7b50ea24b930");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5673b8cf-12de-44f6-92ad-fae4a77932ad",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27645e4c-3463-43ec-b58f-87b67ef4df14", "AQAAAAEAACcQAAAAEBTRPxPx3QP1QYS/NknCgTDk2ivGRmb+UHJYNbm7inFoKQLhrRDDZV8ZHL6M8tGZOw==", "97fe20f7-d478-41d2-a14a-ec601001dba3" });
        }
    }
}
