using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuranHub.DAL.Migrations.Video
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlayListsInfo",
                columns: table => new
                {
                    PlayListInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfVideos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayListsInfo", x => x.PlayListInfoId);
                });

            migrationBuilder.CreateTable(
                name: "VideosData",
                columns: table => new
                {
                    VideoDataId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<int>(type: "int", nullable: false),
                    Width = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayListId = table.Column<int>(type: "int", nullable: false),
                    PlayListInfoId = table.Column<int>(type: "int", nullable: false),
                    Views = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideosData", x => x.VideoDataId);
                    table.ForeignKey(
                        name: "FK_VideosData_PlayListsInfo_PlayListInfoId",
                        column: x => x.PlayListInfoId,
                        principalTable: "PlayListsInfo",
                        principalColumn: "PlayListInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VideosData_PlayListInfoId",
                table: "VideosData",
                column: "PlayListInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideosData");

            migrationBuilder.DropTable(
                name: "PlayListsInfo");
        }
    }
}
