using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medium.DA.Migrations
{
    public partial class MantToManySaveListStory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavingListStory",
                columns: table => new
                {
                    SavingListsId = table.Column<int>(type: "int", nullable: false),
                    StoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingListStory", x => new { x.SavingListsId, x.StoriesId });
                    table.ForeignKey(
                        name: "FK_SavingListStory_SavingLists_SavingListsId",
                        column: x => x.SavingListsId,
                        principalTable: "SavingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_SavingListStory_Stories_StoriesId",
                        column: x => x.StoriesId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SavingListStory_StoriesId",
                table: "SavingListStory",
                column: "StoriesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SavingListStory");
        }
    }
}
