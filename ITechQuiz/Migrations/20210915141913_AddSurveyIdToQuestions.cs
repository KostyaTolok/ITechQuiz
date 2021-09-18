using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITechQuiz.Migrations
{
    public partial class AddSurveyIdToQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "SurveyName",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "a3661210-9231-40c8-9cf6-2c2340ca8b80");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "bf66d905-9581-44b0-b0ad-c1f1dcb89287");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c2a70f55-1ea9-414e-8144-8e99b986d573", "AQAAAAEAACcQAAAAEGmZRtpWlrUwVEgrLbhkbzP3ZLZhbRg5OGOaswg4MCoK3Uk6sgIw2x9uPJg2GiibDA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "75668e1a-f7a3-4bf8-a13a-7ae810b84f55", "AQAAAAEAACcQAAAAENlYAdv9usfx1kdrSuVHKRisdTiOhloBFYZ98mQnQsHg3cE+fXFfnwvGlKCWaor/UA==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions");

            migrationBuilder.AlterColumn<Guid>(
                name: "SurveyId",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "SurveyName",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "c27180bd-4d5c-47d5-9f6d-2fe1f637f981");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "e2d31f2d-b29f-4df8-97bb-8ed54c1fafe9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d056835-0936-4c32-b494-66c39c31fc2e", "AQAAAAEAACcQAAAAEKpjVCXRKRzARHVojoSgS+mKZK/9z6j5XhBrCJyPOMlFypA/AcnfUz6Jf/J6DHHeaw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e52e54e-4274-42fa-be92-692773cb555a", "AQAAAAEAACcQAAAAELe2ziNqDLcqsgzb7aVoSUYZcr3femTXDmYCs23t3TNZ/HGYL1XFSq7F7niaHhB5Rg==" });

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Surveys_SurveyId",
                table: "Questions",
                column: "SurveyId",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
