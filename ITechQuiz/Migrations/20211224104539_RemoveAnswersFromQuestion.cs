using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    public partial class RemoveAnswersFromQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Answers");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "Answers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "6f3052d0-1bd1-4f45-9b22-cf1095f7af22");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "67bd94cd-7c78-4a9c-ae72-0db38c248040");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cac00cc4-60b1-4a91-bf5d-eb6355e078ee", "AQAAAAEAACcQAAAAEO6zhBwdnq28CgF4JjlgicxFWLoiOmag6s0VG2l1shXwk2veLVPUm/0K55ArLfYfcQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "12915641-a9f5-4e63-8772-cdb71490e62e", "AQAAAAEAACcQAAAAENi2bhzpTOBfqX6smu618iZLNbmjzGAOnUFU13Pt05bINl8AuDV3Yn3ks/vmfgHPuQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}
