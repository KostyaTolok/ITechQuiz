using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAnonymousAllowed",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMultipleAnswersAllowed",
                table: "Surveys",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Answers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategorySurvey",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SurveysId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategorySurvey", x => new { x.CategoriesId, x.SurveysId });
                    table.ForeignKey(
                        name: "FK_CategorySurvey_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategorySurvey_Surveys_SurveysId",
                        column: x => x.SurveysId,
                        principalTable: "Surveys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_CategorySurvey_SurveysId",
                table: "CategorySurvey",
                column: "SurveysId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategorySurvey");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "IsAnonymousAllowed",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "IsMultipleAnswersAllowed",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Answers");

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
        }
    }
}
