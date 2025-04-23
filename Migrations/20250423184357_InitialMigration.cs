using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace gamehub_API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isVerified = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Videogames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    platform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    publisher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    releaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videogames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Videogames",
                columns: new[] { "Id", "genre", "image", "platform", "publisher", "rating", "releaseDate", "status", "title" },
                values: new object[,]
                {
                    { 1, "Action-adventure", "https://zelda.nintendo.com/breath-of-the-wild/assets/media/wallpapers/tablet-1.jpg", "Nintendo Switch", "Nintendo", 10, new DateTime(2017, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "The Legend of Zelda: Breath of the Wild" },
                    { 2, "Action-adventure", "https://www.ultimagame.es/god-war-4/imagen-i19055-pge.jpg", "PlayStation 4", "Sony Interactive Entertainment", 10, new DateTime(2018, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "God of War" },
                    { 3, "Action-adventure", "https://i.blogs.es/juegos/13424/red_dead_3__nombre_temporal_/fotos/maestras/red_dead_3__nombre_temporal_-4030936.jpg", "PlayStation 4, Xbox One, PC", "Rockstar Games", 10, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Red Dead Redemption 2" },
                    { 4, "Action RPG", "https://cdn1.epicgames.com/offer/14ee004dadc142faaaece5a6270fb628/EGS_TheWitcher3WildHuntCompleteEdition_CDPROJEKTRED_S2_1200x1600-53a8fb2c0201cd8aea410f2a049aba3f", "PlayStation 4, Xbox One, PC, Nintendo Switch", "CD Projekt", 10, new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "The Witcher 3: Wild Hunt" },
                    { 5, "Action RPG", "https://muropaketti.com/wp-content/uploads/2020/12/cyberpunk-box.jpg", "PlayStation 4, Xbox One, PC, Stadia", "CD Projekt", 10, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Cyberpunk 2077" },
                    { 6, "Sandbox, Survival", "https://p2.trrsf.com/image/fget/cf/1200/1600/middle/images.terra.com/2020/09/30/minecraft-cover-art.jpg", "Multiple platforms", "Mojang Studios", 10, new DateTime(2011, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Minecraft" },
                    { 7, "Battle Royale, Survival", "https://cdn1.epicgames.com/offer/fn/FNBR_34-00_C6S2_EGS_Launcher_KeyArt_FNlogo_Blade_1200x1600_1200x1600-0aa5c6ea35dab419ec28980fdb402e89", "Multiple platforms", "Epic Games", 10, new DateTime(2017, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Fortnite" },
                    { 8, "Battle Royale", "https://static.serlogal.com/imagenes_big/9788467/978846795073.JPG", "PlayStation 4, Xbox One, PC, Nintendo Switch", "Electronic Arts", 10, new DateTime(2019, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Apex Legends" },
                    { 9, "Party, Social Deduction", "https://example.com/amongus.jpg", "PC, Mobile, Nintendo Switch", "Innersloth", 10, new DateTime(2018, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Among Us" },
                    { 10, "Roguelike, Action RPG", "https://example.com/hades.jpg", "PC, Nintendo Switch", "Supergiant Games", 10, new DateTime(2020, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Hades" },
                    { 11, "Action-adventure", "https://example.com/lastofus2.jpg", "PlayStation 4", "Sony Interactive Entertainment", 10, new DateTime(2020, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "The Last of Us Part II" },
                    { 12, "Action-adventure", "https://example.com/ghostoftsushima.jpg", "PlayStation 4", "Sony Interactive Entertainment", 10, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Ghost of Tsushima" },
                    { 13, "Action RPG", "https://example.com/horizonzerodawn.jpg", "PlayStation 4, PC", "Sony Interactive Entertainment", 10, new DateTime(2017, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Horizon Zero Dawn" },
                    { 14, "Action-adventure", "https://example.com/sekiro.jpg", "PlayStation 4, Xbox One, PC", "Activision", 10, new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Sekiro: Shadows Die Twice" },
                    { 15, "Survival horror", "https://example.com/residentevilvillage.jpg", "PlayStation 4, PlayStation 5, Xbox One, Xbox Series X/S, PC", "Capcom", 10, new DateTime(2021, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Released", "Resident Evil Village" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Videogames");
        }
    }
}
