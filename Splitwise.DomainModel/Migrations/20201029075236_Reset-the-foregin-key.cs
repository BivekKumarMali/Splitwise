using Microsoft.EntityFrameworkCore.Migrations;

namespace Splitwise.DomainModel.Migrations
{
    public partial class Resettheforeginkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ExpenseDetails",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_GroupId",
                table: "Expenses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_ExpenseId",
                table: "ExpenseDetails",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseDetails_UserId",
                table: "ExpenseDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_UserId",
                table: "ApplicationUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseDetails_Expenses_ExpenseId",
                table: "ExpenseDetails",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseDetails_ApplicationUsers_UserId",
                table: "ExpenseDetails",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Groups_GroupId",
                table: "Expenses",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ApplicationUsers_UserId",
                table: "Expenses",
                column: "UserId",
                principalTable: "ApplicationUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_AspNetUsers_UserId",
                table: "ApplicationUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseDetails_Expenses_ExpenseId",
                table: "ExpenseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseDetails_ApplicationUsers_UserId",
                table: "ExpenseDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Groups_GroupId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ApplicationUsers_UserId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_GroupId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseDetails_ExpenseId",
                table: "ExpenseDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExpenseDetails_UserId",
                table: "ExpenseDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Expenses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ExpenseDetails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
