using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medium.DA.Migrations
{
    public partial class testSelfRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Publisher_PublisherId",
                table: "Story");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher");

            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "Publishers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FollowerFollowing",
                columns: table => new
                {
                    FollowersId = table.Column<int>(type: "int", nullable: false),
                    FollowingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowerFollowing", x => new { x.FollowersId, x.FollowingsId });
                    table.ForeignKey(
                        name: "FK_FollowerFollowing_Publishers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "Publishers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowerFollowing_Publishers_FollowingsId",
                        column: x => x.FollowingsId,
                        principalTable: "Publishers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowerFollowing_FollowingsId",
                table: "FollowerFollowing",
                column: "FollowingsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Publishers_PublisherId",
                table: "Story",
                column: "PublisherId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Story_Publishers_PublisherId",
                table: "Story");

            migrationBuilder.DropTable(
                name: "FollowerFollowing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Publishers",
                table: "Publishers");

            migrationBuilder.RenameTable(
                name: "Publishers",
                newName: "Publisher");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Publisher",
                table: "Publisher",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Story_Publisher_PublisherId",
                table: "Story",
                column: "PublisherId",
                principalTable: "Publisher",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
