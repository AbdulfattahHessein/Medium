using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medium.DA.Migrations
{
    public partial class addAdminAndUserRolesForAdminAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "83200b68-99f1-4e11-aedb-2833b16682f0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "86e24a3b-0434-4d4d-920a-dac387c68138");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4defe02-03b4-4999-b173-4a986c988fce", "AQAAAAEAACcQAAAAEF/i4gOvDlHEQtLlEQyTscCPymcTYjHb2zf8Lhma0+fEMyaovTq9QY7ae8fp2bO9hA==", "5347cf41-481c-4fef-bfe0-b5d57f50ef1e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "43d86d9a-1f57-45d5-9883-0faf95ad30e4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "09099867-2da1-4b33-b9e3-8a80a24a1007");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa6c29a9-1294-40ac-ae00-ffad612f62bf", "AQAAAAEAACcQAAAAEPuCrYdtU5d6irzop+yLnBTBVryjxvyu5ItlsWEonaLMM7HRKcLzrd5YbRG0Q6xYMQ==", "f220372c-cc68-408a-a128-dad2cd12a540" });
        }
    }
}
