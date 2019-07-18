using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramGenerator.EF.CodeFirst.Migrations
{
    public partial class changeCachedForeignKeyNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_AnagramId",
                table: "CachedWords");

            migrationBuilder.AlterColumn<int>(
                name: "AnagramId",
                table: "CachedWords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_AnagramId",
                table: "CachedWords",
                column: "AnagramId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_AnagramId",
                table: "CachedWords");

            migrationBuilder.AlterColumn<int>(
                name: "AnagramId",
                table: "CachedWords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_AnagramId",
                table: "CachedWords",
                column: "AnagramId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
