using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class ForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "8f87d4c1-da7c-4c41-b86d-58dbde0d4ead");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "9272b80c-8aa1-47c4-ac4f-55a53b1f6142");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "54c4150d-8ccd-4c88-b742-916232378945", "AQAAAAEAACcQAAAAEEXK8TiGZiTPaPZKrH/IGciarFovFUg306dQjkRtlsZKI23TKJI9ea36FWOortUb5g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0638b426-264b-4c09-b4e5-9d47886c6c69", "AQAAAAEAACcQAAAAEN38eXneyycKjedbyIphHb0fxW4+YXkje0edk1Or2G5llD5GffMVUxkL82JRUfJRBg==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "5445ce23-76d0-47de-94ca-aaa97d67a25d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "971383f0-5695-434a-8a44-6780ab45bda9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "26f18440-f8bf-47b9-8935-b05e54aae0ab", "AQAAAAEAACcQAAAAEOubmqEOsTAp6T/I/0MnYypDaYa+GXg4zoeZGlA6rEQjulAmN1XjzS9EZA0aU1W9tw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1cbf742c-b56e-4aa1-b270-188143901b45", "AQAAAAEAACcQAAAAEECaHVHmOYvsHLsYTfuoYRhb43lHTIhc8rGshAr7P6Nrovaie1jAwLwf81/hqbpulA==" });
        }
    }
}
