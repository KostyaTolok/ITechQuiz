using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class AddSurveyUpdatedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Surveys",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "cf349df0-f60d-489e-bc6e-af73f952378e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "ec4fcba8-7657-4a52-85d7-031e4615ce1b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b706b9e8-4e02-4d71-9609-037700e478a4", "AQAAAAEAACcQAAAAEF+ITSsz4hXYJa7+RbCnC9NDIuL1+/hCyVRWoXfiNMu9WyISYfXUzHlk/Pw6ouq07A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d77299c1-109f-4932-9c6b-cf7594491dee", "AQAAAAEAACcQAAAAEBwa+rzOIuVptSo9/wfhQr4idhk8iBRJqS0m7YLpZEZGSuJ6YlnY0msDdi+H1t0TBw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Surveys");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "2deb57ed-6bee-4c80-8191-4233b1c2369a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "4c2f9e21-3f4d-4875-9cf8-69e4f139f5ff");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f41c0434-6bd6-4d4c-a070-a430f70451a2", "AQAAAAEAACcQAAAAEFeBsdDSCGJUUEjF9yoyF0j2OjRu39hx9N4D+H70o7Dgfoa8N0U95xDR4CtobOpk+Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00465ca6-192a-4931-b93f-b657018406b4", "AQAAAAEAACcQAAAAEKU3J+WM+hVzhQYf4Ihp9H/MiLjQyMnZ6F55wbGW0oS85VSqUbwdAt1KvX0USi9Vig==" });
        }
    }
}
