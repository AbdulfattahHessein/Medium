using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medium.DA.Migrations
{
    public partial class intitt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryPhotos_Stories_StoryId",
                table: "StoryPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "StoryPhotos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryPhotos_Stories_StoryId",
                table: "StoryPhotos",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryPhotos_Stories_StoryId",
                table: "StoryPhotos");

            migrationBuilder.AlterColumn<int>(
                name: "StoryId",
                table: "StoryPhotos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryPhotos_Stories_StoryId",
                table: "StoryPhotos",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
