using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatalogArtistNationalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogArtistNationalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogColorPalettes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HexCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogColorPalettes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogMediums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogMediums", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatalogArtists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CatalogArtistNationalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogArtists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogArtists_CatalogArtistNationalities_CatalogArtistNationalityId",
                        column: x => x.CatalogArtistNationalityId,
                        principalTable: "CatalogArtistNationalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PictureFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogArtistId = table.Column<int>(type: "int", nullable: false),
                    CatalogGenreId = table.Column<int>(type: "int", nullable: false),
                    CatalogMediumId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogArtists_CatalogArtistId",
                        column: x => x.CatalogArtistId,
                        principalTable: "CatalogArtists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogGenres_CatalogGenreId",
                        column: x => x.CatalogGenreId,
                        principalTable: "CatalogGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItems_CatalogMediums_CatalogMediumId",
                        column: x => x.CatalogMediumId,
                        principalTable: "CatalogMediums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogItemCatalogColorPalettes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatalogItemId = table.Column<int>(type: "int", nullable: false),
                    CatalogColorPaletteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogItemCatalogColorPalettes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CatalogItemCatalogColorPalettes_CatalogColorPalettes_CatalogColorPaletteId",
                        column: x => x.CatalogColorPaletteId,
                        principalTable: "CatalogColorPalettes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogItemCatalogColorPalettes_CatalogItems_CatalogItemId",
                        column: x => x.CatalogItemId,
                        principalTable: "CatalogItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatalogArtists_CatalogArtistNationalityId",
                table: "CatalogArtists",
                column: "CatalogArtistNationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemCatalogColorPalettes_CatalogColorPaletteId",
                table: "CatalogItemCatalogColorPalettes",
                column: "CatalogColorPaletteId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItemCatalogColorPalettes_CatalogItemId",
                table: "CatalogItemCatalogColorPalettes",
                column: "CatalogItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogArtistId",
                table: "CatalogItems",
                column: "CatalogArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogGenreId",
                table: "CatalogItems",
                column: "CatalogGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogItems_CatalogMediumId",
                table: "CatalogItems",
                column: "CatalogMediumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogItemCatalogColorPalettes");

            migrationBuilder.DropTable(
                name: "CatalogColorPalettes");

            migrationBuilder.DropTable(
                name: "CatalogItems");

            migrationBuilder.DropTable(
                name: "CatalogArtists");

            migrationBuilder.DropTable(
                name: "CatalogGenres");

            migrationBuilder.DropTable(
                name: "CatalogMediums");

            migrationBuilder.DropTable(
                name: "CatalogArtistNationalities");
        }
    }
}
