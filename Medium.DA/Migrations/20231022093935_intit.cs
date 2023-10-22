using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medium.DA.Migrations
{
    public partial class intit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowerFollowing_Publishers_FollowersId",
                table: "FollowerFollowing");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowerFollowing_Publishers_FollowingsId",
                table: "FollowerFollowing");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowerFollowing",
                table: "FollowerFollowing");

            migrationBuilder.RenameTable(
                name: "FollowerFollowing",
                newName: "Follow");

            migrationBuilder.RenameIndex(
                name: "IX_FollowerFollowing_FollowingsId",
                table: "Follow",
                newName: "IX_Follow_FollowingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follow",
                table: "Follow",
                columns: new[] { "FollowersId", "FollowingsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Publishers_FollowersId",
                table: "Follow",
                column: "FollowersId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Publishers_FollowingsId",
                table: "Follow",
                column: "FollowingsId",
                principalTable: "Publishers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Publishers_FollowersId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Publishers_FollowingsId",
                table: "Follow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follow",
                table: "Follow");

            migrationBuilder.RenameTable(
                name: "Follow",
                newName: "FollowerFollowing");

            migrationBuilder.RenameIndex(
                name: "IX_Follow_FollowingsId",
                table: "FollowerFollowing",
                newName: "IX_FollowerFollowing_FollowingsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowerFollowing",
                table: "FollowerFollowing",
                columns: new[] { "FollowersId", "FollowingsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FollowerFollowing_Publishers_FollowersId",
                table: "FollowerFollowing",
                column: "FollowersId",
                principalTable: "Publishers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowerFollowing_Publishers_FollowingsId",
                table: "FollowerFollowing",
                column: "FollowingsId",
                principalTable: "Publishers",
                principalColumn: "Id");
        }
    }
}
