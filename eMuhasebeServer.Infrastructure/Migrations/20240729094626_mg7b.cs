using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg7b : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SideBarLeftMenu_SideBarLeftMenu_SideBarLeftId",
                table: "SideBarLeftMenu");

            migrationBuilder.RenameColumn(
                name: "SideBarLeftId",
                table: "SideBarLeftMenu",
                newName: "ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_SideBarLeftMenu_SideBarLeftId",
                table: "SideBarLeftMenu",
                newName: "IX_SideBarLeftMenu_ParentId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SideBarLeftMenu",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "SideBarLeftMenu",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_SideBarLeftMenu_SideBarLeftMenu_ParentId",
                table: "SideBarLeftMenu",
                column: "ParentId",
                principalTable: "SideBarLeftMenu",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SideBarLeftMenu_SideBarLeftMenu_ParentId",
                table: "SideBarLeftMenu");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "SideBarLeftMenu");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "SideBarLeftMenu",
                newName: "SideBarLeftId");

            migrationBuilder.RenameIndex(
                name: "IX_SideBarLeftMenu_ParentId",
                table: "SideBarLeftMenu",
                newName: "IX_SideBarLeftMenu_SideBarLeftId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SideBarLeftMenu",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_SideBarLeftMenu_SideBarLeftMenu_SideBarLeftId",
                table: "SideBarLeftMenu",
                column: "SideBarLeftId",
                principalTable: "SideBarLeftMenu",
                principalColumn: "Id");
        }
    }
}
