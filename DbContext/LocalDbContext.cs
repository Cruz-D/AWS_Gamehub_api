using Microsoft.EntityFrameworkCore;
using System;
using gamehub_API.Models;

namespace gamehub_API.DbContext
{
    public class LocalDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options)
        {
        }

        public DbSet<Videogame> Videogames { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Videogame>().HasData(
                new Videogame
                {
                    Id = 1,
                    title = "The Legend of Zelda: Breath of the Wild",
                    genre = "Action-adventure",
                    platform = "Nintendo Switch",
                    rating = 10,
                    publisher = "Nintendo",
                    releaseDate = new DateTime(2017, 3, 3),
                    status = "Released",
                    image = "https://zelda.nintendo.com/breath-of-the-wild/assets/media/wallpapers/tablet-1.jpg"
                },
                new Videogame
                {
                    Id = 2,
                    title = "God of War",
                    genre = "Action-adventure",
                    platform = "PlayStation 4",
                    rating = 10,
                    publisher = "Sony Interactive Entertainment",
                    releaseDate = new DateTime(2018, 4, 20),
                    status = "Released",
                    image = "https://www.ultimagame.es/god-war-4/imagen-i19055-pge.jpg"
                },
                new Videogame
                {
                    Id = 3,
                    title = "Red Dead Redemption 2",
                    genre = "Action-adventure",
                    platform = "PlayStation 4, Xbox One, PC",
                    rating = 10,
                    publisher = "Rockstar Games",
                    releaseDate = new DateTime(2018, 10, 26),
                    status = "Released",
                    image = "https://i.blogs.es/juegos/13424/red_dead_3__nombre_temporal_/fotos/maestras/red_dead_3__nombre_temporal_-4030936.jpg"
                },
                new Videogame
                {
                    Id = 4,
                    title = "The Witcher 3: Wild Hunt",
                    genre = "Action RPG",
                    platform = "PlayStation 4, Xbox One, PC, Nintendo Switch",
                    rating = 10,
                    publisher = "CD Projekt",
                    releaseDate = new DateTime(2015, 5, 19),
                    status = "Released",
                    image = "https://cdn1.epicgames.com/offer/14ee004dadc142faaaece5a6270fb628/EGS_TheWitcher3WildHuntCompleteEdition_CDPROJEKTRED_S2_1200x1600-53a8fb2c0201cd8aea410f2a049aba3f"
                },
                new Videogame
                {
                    Id = 5,
                    title = "Cyberpunk 2077",
                    genre = "Action RPG",
                    platform = "PlayStation 4, Xbox One, PC, Stadia",
                    rating = 10,
                    publisher = "CD Projekt",
                    releaseDate = new DateTime(2020, 12, 10),
                    status = "Released",
                    image = "https://muropaketti.com/wp-content/uploads/2020/12/cyberpunk-box.jpg"
                },
                new Videogame
                {
                    Id = 6,
                    title = "Minecraft",
                    genre = "Sandbox, Survival",
                    platform = "Multiple platforms",
                    rating = 10,
                    publisher = "Mojang Studios",
                    releaseDate = new DateTime(2011, 11, 18),
                    status = "Released",
                    image = "https://p2.trrsf.com/image/fget/cf/1200/1600/middle/images.terra.com/2020/09/30/minecraft-cover-art.jpg"
                },
                new Videogame
                {
                    Id = 7,
                    title = "Fortnite",
                    genre = "Battle Royale, Survival",
                    platform = "Multiple platforms",
                    rating = 10,
                    publisher = "Epic Games",
                    releaseDate = new DateTime(2017, 7, 25),
                    status = "Released",
                    image = "https://cdn1.epicgames.com/offer/fn/FNBR_34-00_C6S2_EGS_Launcher_KeyArt_FNlogo_Blade_1200x1600_1200x1600-0aa5c6ea35dab419ec28980fdb402e89"
                },
                new Videogame
                {
                    Id = 8,
                    title = "Apex Legends",
                    genre = "Battle Royale",
                    platform = "PlayStation 4, Xbox One, PC, Nintendo Switch",
                    rating = 10,
                    publisher = "Electronic Arts",
                    releaseDate = new DateTime(2019, 2, 4),
                    status = "Released",
                    image = "https://static.serlogal.com/imagenes_big/9788467/978846795073.JPG"
                },
                new Videogame
                {
                    Id = 9,
                    title = "Among Us",
                    genre = "Party, Social Deduction",
                    platform = "PC, Mobile, Nintendo Switch",
                    rating = 10,
                    publisher = "Innersloth",
                    releaseDate = new DateTime(2018, 6, 15),
                    status = "Released",
                    image = "https://example.com/amongus.jpg"
                },
                new Videogame
                {
                    Id = 10,
                    title = "Hades",
                    genre = "Roguelike, Action RPG",
                    platform = "PC, Nintendo Switch",
                    rating = 10,
                    publisher = "Supergiant Games",
                    releaseDate = new DateTime(2020, 9, 17),
                    status = "Released",
                    image = "https://example.com/hades.jpg"
                },
                new Videogame
                {
                    Id = 11,
                    title = "The Last of Us Part II",
                    genre = "Action-adventure",
                    platform = "PlayStation 4",
                    rating = 10,
                    publisher = "Sony Interactive Entertainment",
                    releaseDate = new DateTime(2020, 6, 19),
                    status = "Released",
                    image = "https://example.com/lastofus2.jpg"
                },
                new Videogame
                {
                    Id = 12,
                    title = "Ghost of Tsushima",
                    genre = "Action-adventure",
                    platform = "PlayStation 4",
                    rating = 10,
                    publisher = "Sony Interactive Entertainment",
                    releaseDate = new DateTime(2020, 7, 17),
                    status = "Released",
                    image = "https://example.com/ghostoftsushima.jpg"
                },
                new Videogame
                {
                    Id = 13,
                    title = "Horizon Zero Dawn",
                    genre = "Action RPG",
                    platform = "PlayStation 4, PC",
                    rating = 10,
                    publisher = "Sony Interactive Entertainment",
                    releaseDate = new DateTime(2017, 2, 28),
                    status = "Released",
                    image = "https://example.com/horizonzerodawn.jpg"
                },
                new Videogame
                {
                    Id = 14,
                    title = "Sekiro: Shadows Die Twice",
                    genre = "Action-adventure",
                    platform = "PlayStation 4, Xbox One, PC",
                    rating = 10,
                    publisher = "Activision",
                    releaseDate = new DateTime(2019, 3, 22),
                    status = "Released",
                    image = "https://example.com/sekiro.jpg"
                },
                new Videogame
                {
                    Id = 15,
                    title = "Resident Evil Village",
                    genre = "Survival horror",
                    platform = "PlayStation 4, PlayStation 5, Xbox One, Xbox Series X/S, PC",
                    rating = 10,
                    publisher = "Capcom",
                    releaseDate = new DateTime(2021, 5, 7),
                    status = "Released",
                    image = "https://example.com/residentevilvillage.jpg"
                }
            );
        }
    }
}
