using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Diary.Data.Migrations
{
    public partial class Create_UserAndDiary_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "personal_diary");

            migrationBuilder.CreateTable(
                name: "users",
                schema: "personal_diary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2019, 9, 5, 15, 41, 1, 686, DateTimeKind.Utc).AddTicks(8339)),
                    name = table.Column<string>(type: "varchar(50)", nullable: false),
                    last_name = table.Column<string>(type: "varchar(40)", nullable: false),
                    nick_name = table.Column<string>(type: "varchar(40)", nullable: false),
                    password = table.Column<string>(type: "varchar(40)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "diaries",
                schema: "personal_diary",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(11)", nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValue: new DateTime(2019, 9, 5, 15, 41, 1, 713, DateTimeKind.Utc).AddTicks(2141)),
                    content = table.Column<string>(type: "varchar(2500)", nullable: false),
                    user_id = table.Column<int>(type: "int(11)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diaries", x => x.id);
                    table.ForeignKey(
                        name: "FK_diaries_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "personal_diary",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_diaries_user_id",
                schema: "personal_diary",
                table: "diaries",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "diaries",
                schema: "personal_diary");

            migrationBuilder.DropTable(
                name: "users",
                schema: "personal_diary");
        }
    }
}
