using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechQuiz.Migrations
{
    public partial class AddIdToSurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyName",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyName",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Surveys",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Surveys",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "SurveyName",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SurveyId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Options",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c408920-31d7-45a5-8f8a-a473f5760d85",
                column: "ConcurrencyStamp",
                value: "5731d82b-3b7e-4a97-9077-def6e8ce329a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7d36113-51ff-4b07-8b5f-64fccc8091d5",
                column: "ConcurrencyStamp",
                value: "a8e75015-c53f-43ed-abe8-d590d65eac17");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb96bb35-90fd-4f70-99a0-954fcfb14baf",
                column: "ConcurrencyStamp",
                value: "59e8aaf5-4641-4846-b6e7-1ccd6ee814cd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d91046a9-d12b-4c14-9810-ac3af195066a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ee49b1a-1227-4ce6-99d4-d2dc6d91a1f0", "AQAAAAEAACcQAAAAEHtGRp5qmFgGGXKZUc40PVIbbGDEiBdusAsokfTiX/YLHDvi/gX+SHjHnDN2FAjHFw==", "2720fb9d-f19a-4175-8f09-f36a9fee13f8" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions",
                column: "SurveyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Questions_SurveyId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "SurveyId",
                table: "Questions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Surveys",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "SurveyName",
                table: "Questions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "QuestionId",
                table: "Options",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                table: "Surveys",
                column: "Name");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c408920-31d7-45a5-8f8a-a473f5760d85",
                column: "ConcurrencyStamp",
                value: "8ae347da-0205-46ba-98da-53d0f1489262");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7d36113-51ff-4b07-8b5f-64fccc8091d5",
                column: "ConcurrencyStamp",
                value: "5b079f6f-8c5c-45dd-a7a1-91c8111f1251");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb96bb35-90fd-4f70-99a0-954fcfb14baf",
                column: "ConcurrencyStamp",
                value: "611d11a9-993a-4183-a0e5-e3f1831c3da7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d91046a9-d12b-4c14-9810-ac3af195066a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "14303086-242c-4ff6-87f8-8def87e7a7e5", "AQAAAAEAACcQAAAAEAmwrIssmYGiGsp+BEOdbS9fGE/jA18K4nC8JODEZKyeifj10dR8tLvGKZ1AAzuDTA==", "3b242181-fdec-4750-ab5d-ef29e409f6d3" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_SurveyName",
                table: "Questions",
                column: "SurveyName");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_Questions_QuestionId",
                table: "Options",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyName",
                table: "Questions",
                column: "SurveyName",
                principalTable: "Surveys",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
