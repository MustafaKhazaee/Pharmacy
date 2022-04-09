using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class b : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2d287c0c-1684-42af-b403-298d06b57509"));

            migrationBuilder.RenameColumn(
                name: "Mobile2",
                table: "Employees",
                newName: "Photo");

            migrationBuilder.RenameColumn(
                name: "Mobile1",
                table: "Employees",
                newName: "Mobile");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "IsDeleted", "IsLocked", "LastModifiedBy", "LastModifiedDate", "Password", "Role", "Salt", "UserName" },
                values: new object[] { new Guid("75e0b482-4272-4adf-80f7-56a455936899"), "A", new DateTime(2022, 4, 8, 7, 28, 25, 167, DateTimeKind.Utc).AddTicks(2679), null, null, "mustafa.khazaee1@gmail.com", false, false, null, null, "f5e1e1ed8984fd084565ba5a232093b8febe8039ab3afb6ac6b7d2ee9e26136f", 0, "fde62bb06a4deea329d4aa6b319b2cc4", "mustafa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("75e0b482-4272-4adf-80f7-56a455936899"));

            migrationBuilder.RenameColumn(
                name: "Photo",
                table: "Employees",
                newName: "Mobile2");

            migrationBuilder.RenameColumn(
                name: "Mobile",
                table: "Employees",
                newName: "Mobile1");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "IsDeleted", "IsLocked", "LastModifiedBy", "LastModifiedDate", "Password", "Role", "Salt", "UserName" },
                values: new object[] { new Guid("2d287c0c-1684-42af-b403-298d06b57509"), "A", new DateTime(2022, 4, 7, 12, 38, 17, 243, DateTimeKind.Utc).AddTicks(5983), null, null, "mustafa.khazaee1@gmail.com", false, false, null, null, "08b90fecfc7fc25ab983647a85cdd4d20beee6cdf40a74edf8a3367586f5a921", 0, "492460a881ced1b7609e567f97ac0fdd", "Mustafa" });
        }
    }
}
