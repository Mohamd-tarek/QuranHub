using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuranHub.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hizbs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hizbs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IbnKatheer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IbnKatheer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jalalayn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jalalayn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Juzs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juzs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manzils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manzils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MindMaps",
                columns: table => new
                {
                    MindMapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    MapImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MindMaps", x => x.MindMapId);
                });

            migrationBuilder.CreateTable(
                name: "Muyassar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Muyassar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qortobi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qortobi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Quran",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quran", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuranClean",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuranClean", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rukus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rukus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sajdas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sajdas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suras",
                columns: table => new
                {
                    SuraId = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Ayas = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Rukus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suras", x => x.SuraId);
                });

            migrationBuilder.CreateTable(
                name: "Tabary",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tabary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Translation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Sura = table.Column<int>(type: "int", nullable: false),
                    Aya = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WeightVectorDimentions",
                columns: table => new
                {
                    WeightVectorDimentionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Word = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightVectorDimentions", x => x.WeightVectorDimentionId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hizbs");

            migrationBuilder.DropTable(
                name: "IbnKatheer");

            migrationBuilder.DropTable(
                name: "Jalalayn");

            migrationBuilder.DropTable(
                name: "Juzs");

            migrationBuilder.DropTable(
                name: "Manzils");

            migrationBuilder.DropTable(
                name: "MindMaps");

            migrationBuilder.DropTable(
                name: "Muyassar");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Qortobi");

            migrationBuilder.DropTable(
                name: "Quran");

            migrationBuilder.DropTable(
                name: "QuranClean");

            migrationBuilder.DropTable(
                name: "Rukus");

            migrationBuilder.DropTable(
                name: "Sajdas");

            migrationBuilder.DropTable(
                name: "Suras");

            migrationBuilder.DropTable(
                name: "Tabary");

            migrationBuilder.DropTable(
                name: "Translation");

            migrationBuilder.DropTable(
                name: "WeightVectorDimentions");
        }
    }
}
