using HospitalManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlTypes;
//оч розовый #e055b9
//слабо розовый #ffe8f8
namespace HospitalManager.Data
{
    public class Seed
    {
        public static async Task SeedReceptions(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                var doctor1 = await userManager.FindByEmailAsync("specialist_user@mail.ru");
                var doctor2 = await userManager.FindByEmailAsync("specialist_user2@mail.ru");
                var doctor3 = await userManager.FindByEmailAsync("specialist_user3@mail.ru");
                var doctor4 = await userManager.FindByEmailAsync("specialist_user4@mail.ru");
                var doctor5 = await userManager.FindByEmailAsync("specialist_user5@mail.ru");
                var doctor6 = await userManager.FindByEmailAsync("specialist_user6@mail.ru");
                var doctor7 = await userManager.FindByEmailAsync("specialist_user7@mail.ru");

                for (int i = 0; i < 100; i++)
                {
                    context.Receptions.AddRange(new List<Reception>()
                    {
                        //Парикмахер
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 12, 0, 0).AddDays(i),
                            Price = 700,
                            Procedure = "Стрижка и укладка волос",
                            DoctorId = await userManager.GetUserIdAsync(doctor1),
                            Doctor = doctor1
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 14, 0, 0).AddDays(i),
                            Price = 4000,
                            Procedure = "Уход за волосами и кожей головы",
                            DoctorId = await userManager.GetUserIdAsync(doctor1),
                            Doctor = doctor1
                        },
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 18, 0, 0).AddDays(i),
                            Price = 5000,
                            Procedure = "Окрашивание волос",
                            DoctorId = await userManager.GetUserIdAsync(doctor1),
                            Doctor = doctor1
                        },
                        //Визажист
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 12, 0, 0).AddDays(i),
                            Price = 2000,
                            Procedure = "Повседневный макияж",
                            DoctorId = await userManager.GetUserIdAsync(doctor2),
                            Doctor = doctor2
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 18, 0, 0).AddDays(i),
                            Price = 4000,
                            Procedure = "Вечерний/выходной макияж",
                            DoctorId = await userManager.GetUserIdAsync(doctor2),
                            Doctor = doctor2
                        },
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 10, 0, 0).AddDays(i),
                            Price = 7000,
                            Procedure = "Свадебный макияж",
                            DoctorId = await userManager.GetUserIdAsync(doctor2),
                            Doctor = doctor2
                        },
                        //Косметолог
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 12, 0, 0).AddDays(i),
                            Price = 1700,
                            Procedure = "Чистка лица и уход за кожей ",
                            DoctorId = await userManager.GetUserIdAsync(doctor3),
                            Doctor = doctor3
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 14, 0, 0).AddDays(i),
                            Price = 2000,
                            Procedure = "Ультразвуковая чистка лица",
                            DoctorId = await userManager.GetUserIdAsync(doctor3),
                            Doctor = doctor3
                        },
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 18, 0, 0).AddDays(i),
                            Price = 5000,
                            Procedure = "Процедуры регенерации кожи лица",
                            DoctorId = await userManager.GetUserIdAsync(doctor3),
                            Doctor = doctor3
                        },
                        //Массажист
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 12, 0, 0).AddDays(i),
                            Price = 3000,
                            Procedure = "Классический массаж",
                            DoctorId = await userManager.GetUserIdAsync(doctor4),
                            Doctor = doctor4
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 14, 0, 0).AddDays(i),
                            Price = 3500,
                            Procedure = "Релаксационный массаж",
                            DoctorId = await userManager.GetUserIdAsync(doctor4),
                            Doctor = doctor4
                        },
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 18, 0, 0).AddDays(i),
                            Price = 3250,
                            Procedure = "Лимфодренажный массаж",
                            DoctorId = await userManager.GetUserIdAsync(doctor4),
                            Doctor = doctor4
                        },
                        //Бровист
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 12, 0, 0).AddDays(i),
                            Price = 1500,
                            Procedure = "Коррекция и окрашивание бровей",
                            DoctorId = await userManager.GetUserIdAsync(doctor5),
                            Doctor = doctor5
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 14, 0, 0).AddDays(i),
                            Price = 2250,
                            Procedure = "Микроблейдинг бровей",
                            DoctorId = await userManager.GetUserIdAsync(doctor5),
                            Doctor = doctor5
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 18, 0, 0).AddDays(i),
                            Price = 800,
                            Procedure = "Хнойное окрашивание бровей",
                            DoctorId = await userManager.GetUserIdAsync(doctor5),
                            Doctor = doctor5
                        },
                        //Специалист по эпиляции
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 12, 0, 0).AddDays(i),
                            Price = 2500,
                            Procedure = "Восковая депиляция",
                            DoctorId = await userManager.GetUserIdAsync(doctor6),
                            Doctor = doctor6
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 14, 0, 0).AddDays(i),
                            Price = 1400,
                            Procedure = "Шугаринг (сахарная депиляция)",
                            DoctorId = await userManager.GetUserIdAsync(doctor6),
                            Doctor = doctor6
                        },
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 18, 0, 0).AddDays(i),
                            Price = 5000,
                            Procedure = "Лазерная эпиляция",
                            DoctorId = await userManager.GetUserIdAsync(doctor6),
                            Doctor = doctor6
                        },
                        //SPA-терапевт
                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 12, 0, 0).AddDays(i),
                            Price = 1350,
                            Procedure = "Релаксационный SPA-массаж",
                            DoctorId = await userManager.GetUserIdAsync(doctor7),
                            Doctor = doctor7
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 14, 0, 0).AddDays(i),
                            Price = 6000,
                            Procedure = "SPA-уход за телом",
                            DoctorId = await userManager.GetUserIdAsync(doctor7),
                            Doctor = doctor7
                        },

                        new Reception()
                        {
                            Date = new DateTime(2024, 6, 8, 18, 0, 0).AddDays(i),
                            Price = 12000,
                            Procedure = "SPA-процедуры для расслабления и оздоровления",
                            DoctorId = await userManager.GetUserIdAsync(doctor7),
                            Doctor = doctor7
                        }
                    });
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                if (!await roleManager.RoleExistsAsync(UserRoles.Doctor))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Doctor));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "admin_user@mail.ru";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        Name = "Александра",
                        Surname = "Семенюк",
                        MiddleName = "Владимировна",
                        UserName = "sasha_admin",
                        BirthDay = new DateTime(2004, 9, 29),
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "Nikitos228-");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "app_user@mail.ru";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        Name = "Роман",
                        Surname = "Иванов",
                        MiddleName = "Олегович",
                        BirthDay = new DateTime(2000, 9, 19),
                        UserName = "app_user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Nikitos228-");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
                //1 доктор
                string doctorUserEmail = "specialist_user@mail.ru";
                var doctorUser = await userManager.FindByEmailAsync(doctorUserEmail);
                if (doctorUser == null)
                {
                    var newDoctorUser = new AppUser()
                    {
                        Name = "Александр",
                        Surname = "Петров",
                        MiddleName = "Иванович",
                        BirthDay = new DateTime(1998, 3, 13),
                        UserName = "specialist_user",
                        Specialization = "Парикхмахер",
                        Email = doctorUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newDoctorUser, "Nikitos228-");
                    await userManager.AddToRoleAsync(newDoctorUser, UserRoles.Doctor);
                }
                //2 доктор
                string doctorUser2Email = "specialist_user2@mail.ru";

                var doctorUser2 = await userManager.FindByEmailAsync(doctorUser2Email);
                if (doctorUser2 == null)
                {
                    var newDoctorUser2 = new AppUser()
                    {
                        Name = "Екатерина",
                        Surname = "Смирнова",
                        MiddleName = "Сергеевна",
                        BirthDay = new DateTime(1990, 2, 1),
                        UserName = "specialist_user2",
                        Specialization = "Визажист",
                        Email = doctorUser2Email,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newDoctorUser2, "Nikitos228-");
                    await userManager.AddToRoleAsync(newDoctorUser2, UserRoles.Doctor);
                }

                //3 доктор
                string doctorUser3Email = "specialist_user3@mail.ru";

                var doctorUser3 = await userManager.FindByEmailAsync(doctorUser3Email);
                if (doctorUser3 == null)
                {
                    var newDoctorUser3 = new AppUser()
                    {
                        Name = "Дмитрий",
                        Surname = "Козлов",
                        MiddleName = "Николаевич",
                        BirthDay = new DateTime(1985, 5, 20),
                        UserName = "specialist_user3",
                        Specialization = "Косметолог",
                        Email = doctorUser3Email,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newDoctorUser3, "Nikitos228-");
                    await userManager.AddToRoleAsync(newDoctorUser3, UserRoles.Doctor);
                }

                //4 доктор
                string doctorUser4Email = "specialist_user4@mail.ru";

                var doctorUser4 = await userManager.FindByEmailAsync(doctorUser4Email);
                if (doctorUser4 == null)
                {
                    var newDoctorUser4 = new AppUser()
                    {
                        Name = "Ольга",
                        Surname = "Морозова",
                        MiddleName = "Алексеевна",
                        BirthDay = new DateTime(1991, 7, 28),
                        UserName = "specialist_user4",
                        Specialization = "Массажист",
                        Email = doctorUser4Email,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newDoctorUser4, "Nikitos228-");
                    await userManager.AddToRoleAsync(newDoctorUser4, UserRoles.Doctor);
                }

                //5 доктор
                string doctorUser5Email = "specialist_user5@mail.ru";

                var doctorUser5 = await userManager.FindByEmailAsync(doctorUser5Email);
                if (doctorUser5 == null)
                {
                    var newDoctorUser5 = new AppUser()
                    {
                        Name = "Иван",
                        Surname = "Кузнецов",
                        MiddleName = "Павлович",
                        BirthDay = new DateTime(1975, 2, 14),
                        UserName = "specialist_user5",
                        Specialization = "Бровист",
                        Email = doctorUser5Email,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newDoctorUser5, "Nikitos228-");
                    await userManager.AddToRoleAsync(newDoctorUser5, UserRoles.Doctor);
                }
                //6 доктор
                string doctorUser6Email = "specialist_user6@mail.ru";

                var doctorUser6 = await userManager.FindByEmailAsync(doctorUser6Email);
                if (doctorUser6 == null)
                {
                    var newDoctorUser6 = new AppUser()
                    {
                        Name = "Татьяна",
                        Surname = "Васильева",
                        MiddleName = "Владимировна",
                        BirthDay = new DateTime(1991, 7, 28),
                        UserName = "specialist_user6",
                        Specialization = "Специалист по эпиляции",
                        Email = doctorUser6Email,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newDoctorUser6, "Nikitos228-");
                    await userManager.AddToRoleAsync(newDoctorUser6, UserRoles.Doctor);
                }
                //7 доктор
                string doctorUser7Email = "specialist_user7@mail.ru";

                var doctorUser7 = await userManager.FindByEmailAsync(doctorUser7Email);
                if (doctorUser7 == null)
                {
                    var newDoctorUser7 = new AppUser()
                    {
                        Name = "Сергей",
                        Surname = "Попов",
                        MiddleName = "Викторович",
                        BirthDay = new DateTime(1983, 1, 1),
                        UserName = "specialist_user7",
                        Specialization = "SPA-терапевт",
                        Email = doctorUser7Email,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newDoctorUser7, "Nikitos228-");
                    await userManager.AddToRoleAsync(newDoctorUser7, UserRoles.Doctor);
                }
            }
        }
    }
    public class Delete
    {
        public static void DeleteTable(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureDeleted();
                context.SaveChanges();
            }
        }

        public static void ClearReceptions(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (context.Receptions.Any())
                {
                    context.Receptions.RemoveRange(context.Receptions);
                    context.SaveChanges();
                }
                else
                {
                    SqlNullValueException sqlnullvalue = new SqlNullValueException("Таблица уже пустая");
                    throw sqlnullvalue;
                }
            }
        }

        public static void ClearUsers(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                if (context.Users.Any())
                {
                    context.Users.RemoveRange(context.Users);
                    context.SaveChanges();
                }
                else
                {
                    SqlNullValueException sqlnullvalue = new SqlNullValueException("Таблица уже пустая");
                    throw sqlnullvalue;
                }
            }
        }
    }
}
