using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class Answers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsAnonymous = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerOption",
                columns: table => new
                {
                    AnswersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SelectedOptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerOption", x => new { x.AnswersId, x.SelectedOptionsId });
                    table.ForeignKey(
                        name: "FK_AnswerOption_Answers_AnswersId",
                        column: x => x.AnswersId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnswerOption_Options_SelectedOptionsId",
                        column: x => x.SelectedOptionsId,
                        principalTable: "Options",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f7d36113-51ff-4b07-8b5f-64fccc8091d5"),
                column: "ConcurrencyStamp",
                value: "9d8fc006-74de-4889-a10c-b273757b25c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fb96bb35-90fd-4f70-99a0-954fcfb14baf"),
                column: "ConcurrencyStamp",
                value: "cc9a9b5f-875a-4c55-b786-55b6580a7277");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("52f4c7c6-7f95-4d40-8308-b36a3ce86a52"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a774db3f-b1e6-4866-875d-0339c59ec647", "AQAAAAEAACcQAAAAEHegpOSsVcAryUZO6kmpQr+f96QQrF4tvrXU0HAjhj/Fw2OdNKKQQ1k7PTE2plorOg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d91046a9-d12b-4c14-9810-ac3af195066a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c7f8227d-f65b-4cd3-b25e-72d538d223e7", "AQAAAAEAACcQAAAAEBzb0q/af+QVp4PmmlOrCdTd9Nci7fLqNCy51mk3N7kvxj02ellAetY2d4ou8k6oBQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerOption_SelectedOptionsId",
                table: "AnswerOption",
                column: "SelectedOptionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerOption");

            migrationBuilder.DropTable(
                name: "Answers");

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
    }
}
