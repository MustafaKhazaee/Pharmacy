using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class c : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("75e0b482-4272-4adf-80f7-56a455936899"));

            migrationBuilder.AddColumn<string>(
                name: "PhotoResized",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "IsDeleted", "IsLocked", "LastModifiedBy", "LastModifiedDate", "Password", "Role", "Salt", "UserName" },
                values: new object[] { new Guid("5bacff71-c9f9-42f3-9d35-ec0b97aded0c"), "A", new DateTime(2022, 4, 8, 11, 48, 43, 459, DateTimeKind.Utc).AddTicks(1428), null, null, "mustafa.khazaee1@gmail.com", false, false, null, null, "f23baa9e8be648bda570198617d01d0151d84e91e519327b9f32e4d55f9f23ce", 0, "b3fdbfd2006252ee7628a687f0f81740", "mustafa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("5bacff71-c9f9-42f3-9d35-ec0b97aded0c"));

            migrationBuilder.DropColumn(
                name: "PhotoResized",
                table: "Employees");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "Email", "IsDeleted", "IsLocked", "LastModifiedBy", "LastModifiedDate", "Password", "Role", "Salt", "UserName" },
                values: new object[] { new Guid("75e0b482-4272-4adf-80f7-56a455936899"), "A", new DateTime(2022, 4, 8, 7, 28, 25, 167, DateTimeKind.Utc).AddTicks(2679), null, null, "mustafa.khazaee1@gmail.com", false, false, null, null, "f5e1e1ed8984fd084565ba5a232093b8febe8039ab3afb6ac6b7d2ee9e26136f", 0, "fde62bb06a4deea329d4aa6b319b2cc4", "mustafa" });
        }
    }
}
