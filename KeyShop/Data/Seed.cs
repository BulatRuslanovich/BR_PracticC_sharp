using KeyShop.Data.Enum;
using KeyShop.Models;
using Microsoft.AspNetCore.Identity;

namespace KeyShop.Data {
    public class Seed {
        public static void SeedData(IApplicationBuilder applicationBuilder) {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

                context.Database.EnsureCreated();

                if (!context.Games.Any()) {
                    context.Games.AddRange(new List<Game>() {
                        new Game() {
                            Title = "Wither 3",
                            Description =
                                "«Ведьма́к 3: Дикая Охота» (польск. Wiedźmin 3: Dziki Gon, англ. The Witcher 3: Wild Hunt) — компьютерная игра в жанре action/RPG, разработанная и изданная польской студией CD Projekt RED. Изначально игра была выпущена 19 мая 2015 года на Windows, PlayStation 4 и Xbox One, затем 15 октября 2019 года на Nintendo Switch, а 14 декабря 2022 года — на PlayStation 5 и Xbox Series X/S. Является продолжением игр «Ведьмак» (2007) и «Ведьмак 2: Убийцы королей» (2011). Это третья игра, действие которой происходит в литературной вселенной книжной серии «Ведьмак», созданной польским писателем Анджеем Сапковским, а также последняя, которая повествует о приключениях Геральта из Ривии.",
                            Images =
                                {
                                    "https://w.forfun.com/fetch/a5/a5c0b3b45df3e23788c6130b118535a4.jpeg",
                                    "https://kingame.ru/wp-content/uploads/b/1/f/b1f10764f82b896d4bb9e1983caa6cc7.png",
                                    "https://phonoteka.org/uploads/posts/2023-03/1679380158_phonoteka-org-p-vedmak-art-art-krasivo-89.jpg",
                                    "https://darkstalker.ru/wp-content/uploads/0/c/c/0cc854bc6d507b6c383d89f21ffd74ec.jpeg",
                                    "https://www.allmmorpg.ru/wp-content/uploads/2023/07/5447676767-optimized.jpg",
                                },

                            Publisher = "CD Projekt RED",
                            Developer = "CD Projekt RED",
                            Platform = Platform.PC,
                            Genre = Genre.Adventure,
                            Price = 2459,

                        },

                        new Game() {
                            Title = "Terraria",
                            Description =
                                "Копайте, сражайтесь, исследуйте, стройте! Нет ничего невозможного в этой насыщенной событиями приключенческой игре. Весь мир — ваше полотно, а вся земля — ваши краски! Хватайте инструменты и вперед! Создавайте оружие, чтобы сражаться с различными врагами в разных биомах.",
                            Images =
                                { "https://i.playground.ru/p/ieeQIlrtxi-N2TlNT9LQpg.jpeg",
                                  "https://kartinki.pics/uploads/posts/2022-02/1645953284_28-kartinkin-net-p-kartinki-terrariya-30.jpg",
                                  "https://i.pinimg.com/originals/bb/73/a7/bb73a7f451ec142ab48ce45ad950ff7f.jpg",
                                  "https://files.vgtimes.ru/download/posts/2022-10/1667026460_1510127837_terraria-wallpaper.jpg",
                                  "https://gamegula.org/images/2/5/img_473_4.jpg" },
                            Platform = Platform.PlayStation,
                            Genre = Genre.Adventure,
                            Price = 459,

                            Publisher = "Re-Logic",
                            Developer = "Re-Logic",
                        },

                        new Game() {
                            Title = "Elden Ring",
                            Description =
                                "Восстань, погасшая душа! Междуземье ждёт своего повелителя. Пусть благодать приведёт тебя к Кольцу Элден.",
                            Images =
                                { "https://3dnews.ru/assets/external/illustrations/2022/02/11/1059996/1.jpg",
                                  "https://cdn.shazoo.ru/597018_tfD3iBG_steamuserimages-aakamaihd.jpg",
                                  "https://volsiz.ru/wp-content/uploads/2022/09/elden-ring-8-great-armor-sets-for-the-early-game_631d48f49ec26.jpeg",
                                  "https://images.stopgame.ru/news/2022/04/07/hOkt8NoV.jpg",
                                  "https://gamearenda.ru/uploads/posts/2023-01/w844twsyvv7jnkswb6ma9j.jpg" },
                            Platform = Platform.PC,
                            Genre = Genre.Adventure,
                            Price = 4059,

                            Publisher = "FromSoftware Inc.",
                            Developer = "FromSoftware Inc., Bandai Namco Entertainment",
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder) {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope()) {
                // Roles
                var roleManager =
                    serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin)) {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (!await roleManager.RoleExistsAsync(UserRoles.User)) {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                // Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "bulatruslanovich@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

                if (adminUser == null) {
                    var newAdminUser = new User() {
                        UserName = "BulatRusanovich",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Balance = 10000,
                    };

                    await userManager.CreateAsync(newAdminUser, "!Bulat20040923");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string UserEmail = "user@gmail.com";

                var appUser = await userManager.FindByEmailAsync(UserEmail);
                if (true) {
                    var newAppUser = new User() {
                        UserName = "user",
                        Email = UserEmail,
                        EmailConfirmed = true,
                        Balance = 0,
                    };
                    await userManager.CreateAsync(newAppUser, "!Bulat20040923");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
