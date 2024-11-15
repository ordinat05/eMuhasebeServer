using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mg10KatcizelgeAndHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sortIndex",
                table: "FileContents2",
                newName: "SortIndex");

            migrationBuilder.CreateTable(
                name: "KatCizelgeHeaders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    No = table.Column<int>(type: "int", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EditCount = table.Column<int>(type: "int", nullable: false),
                    HaveTblIlceId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KatCizelgeHeaders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KatCizelgeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KatAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DaireAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KatCizelgeHeaderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KatCizelgeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KatCizelgeler_KatCizelgeHeaders_KatCizelgeHeaderId",
                        column: x => x.KatCizelgeHeaderId,
                        principalTable: "KatCizelgeHeaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KatCizelgeler_KatCizelgeHeaderId",
                table: "KatCizelgeler",
                column: "KatCizelgeHeaderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KatCizelgeler");

            migrationBuilder.DropTable(
                name: "KatCizelgeHeaders");

            migrationBuilder.RenameColumn(
                name: "SortIndex",
                table: "FileContents2",
                newName: "sortIndex");
        }
    }
}
