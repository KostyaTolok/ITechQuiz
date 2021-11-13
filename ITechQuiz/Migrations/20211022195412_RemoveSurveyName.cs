using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class RemoveSurveyName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Surveys");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "891df178-e2b4-4a35-8d8e-a32f32438b5f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "48c643de-c89e-4f87-bd5b-18dfe6ebbc3c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7c823473-963c-4196-83c8-1c994e6cc3ef", "AQAAAAEAACcQAAAAELqszylyDK9HeAwY9m0JLSrdpFHpwq5w1a18AuoM+3kUr0Dbl5WrWIOnCd0nX1aiaA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "aee8f1ec-24de-4699-beb0-607354089768", "AQAAAAEAACcQAAAAEOXqyNBLAGabk3dtalUbODT5IOG0e1l0FvhRZToGRfq11YYGcXOf0qJ3ud2dikAQsA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Surveys",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

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
    }
}
