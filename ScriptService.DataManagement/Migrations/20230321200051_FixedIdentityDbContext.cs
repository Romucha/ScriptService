using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ScriptService.DataManagement.Migrations
{
    /// <inheritdoc />
    public partial class FixedIdentityDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "098b5fac-9319-43b8-97de-533d62a5116e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "899d8222-f55f-4286-98a9-57c7086450ab");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d43ff856-fc70-4d43-9e7a-98a93bff4e77", "1d4c1aee-3407-4691-bde2-b83182007561", "Admin", "ADMIN" },
                    { "e30cdc18-3788-4e0c-8dcf-7aad90fdfce2", "968d2c48-f2e5-4eba-a26c-b6d653812cdd", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d43ff856-fc70-4d43-9e7a-98a93bff4e77");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e30cdc18-3788-4e0c-8dcf-7aad90fdfce2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "098b5fac-9319-43b8-97de-533d62a5116e", "01108e17-8e45-4e42-9cf7-c9b2c7671391", "Admin", "ADMIN" },
                    { "899d8222-f55f-4286-98a9-57c7086450ab", "84c8f416-fe57-4501-aecb-a55a81f39106", "User", "USER" }
                });
        }
    }
}
