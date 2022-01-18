using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    public partial class Notifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "d042573f-f1cb-4152-a95d-31094234014e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "d9076ecb-47f8-4651-aafc-02acac300ba5");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "20b951c3-f754-4a72-b8ee-f24a62bcbde5", "AQAAAAEAACcQAAAAENp5KPz0z8xANCGtw2QNx3wMS9KQ6PR0Fdyd9fkk/2oc7iSkogv1DWrQj6dsHCkfug==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d4760d0f-7d9d-42ab-8fe4-54080a4b23af", "AQAAAAEAACcQAAAAEPd1t5Y0iFKYWGTmRvNmZG01dYAOLa6JbSSDD+gen2cc9fYF583eT/4QwvpcEoLy5A==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "1a42f5a4-2d1c-457b-a002-d588eebfc9ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "dbc710ff-de4d-4bec-bbfe-8a8d66ae6f80");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b0678e27-e837-45be-aa0e-59229359dbc5", "AQAAAAEAACcQAAAAEFyrhcXF7aeFdRa87FqWNfCAQ5ufnR5wP4HkV41qqz67p7U9QJSVZfFW/arO9cOsdw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a8453a70-fb44-476a-aa16-96d2ff63cc5a", "AQAAAAEAACcQAAAAEIUzNqxkp5elbDzJBnQx0M7vfXlp1cNc8x21ZDUhhWgsqiFl5SuQTvP1KmdXquEA4Q==" });
        }
    }
}
