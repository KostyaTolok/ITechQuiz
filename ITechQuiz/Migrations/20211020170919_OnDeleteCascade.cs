using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class OnDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "abf30b81-8149-4082-bf27-6a2a6fa073e7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "8f7c348a-68c9-4aba-8c27-6b0d925bf8fa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2eab55bd-9e45-4810-9703-7170e2ac4d20", "AQAAAAEAACcQAAAAEJDoCx5kFaHFaTUTbDNyaApcZywpaUs17NNmwGM+0ospRA/RuzN7wtVgFEHIVbdjGQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ef20ec72-3708-4323-a5d9-f284731a87ae", "AQAAAAEAACcQAAAAEMsSKMWJcqYWadNIcvVnbFL7xV3LgV2aUez5KrUh7Tjhxo4PkFCntB9yRIp71rUq5A==" });
        }
    }
}
