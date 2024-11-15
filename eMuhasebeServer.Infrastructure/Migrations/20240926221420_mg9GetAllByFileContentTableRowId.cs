using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg9GetAllByFileContentTableRowId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "FileContents2");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "FileContents2",
                newName: "sortIndex");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sortIndex",
                table: "FileContents2",
                newName: "Order");

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "FileContents2",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
