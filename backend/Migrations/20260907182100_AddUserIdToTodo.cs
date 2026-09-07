using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace plzwork.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToTodo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Todos",
                type: "integer",
                nullable: true);
            
            migrationBuilder.Sql("""
                UPDATE "Todos"
                SET "UserId" = (SELECT "Id" FROM "Users" ORDER BY "Id" LIMIT 1)
                WHERE "UserId" IS NULL;
                """);

            migrationBuilder.AlterColumn<int>(
                    name: "UserId",
                    table: "Todos",
                    type: "integer",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "integer",
                    oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Todos_UserId",
                table: "Todos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Users_UserId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_UserId",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todos");
        }
    }
}
