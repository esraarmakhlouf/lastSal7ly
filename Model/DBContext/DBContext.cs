using Microsoft.EntityFrameworkCore;
using Model;
using System;

namespace Model
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options)
        : base(options)
        {
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                       .HasOne(a => a.Technical)
                       .WithOne(b => b.User)
                       .HasForeignKey<Technical>(b => b.UsersId);


            #region index fields

            #region customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.Mobile).IsUnique();
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
            });
            #endregion

            #region user
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.Mobile).IsUnique();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
            });
            #endregion
            #endregion

            #region Initial Data

            #region groups
            modelBuilder.Entity<Groups>().HasData(new Groups
            {
                Id = 1,
                Code = "GR-1",
                ArabicName = "مجموعة عامة",
                EnglishName = "General Group",
                CreationDate = DateTime.Now,
                IsMaster = true
            });
            #endregion

            #region users
            modelBuilder.Entity<Users>().HasData(new Users
            {
                Id = 1,
                Code = "US-1",
                UserName = "AdminDev",
                Password = "4VCsPmt2m2ChrhC2k/i+hw==",
                ImageName = "default.jpg",
                Email = "exm@gmail.com",
                Mobile = "1234",
                CreationDate = DateTime.Now,
                IsMaster = true
            });
            modelBuilder.Entity<Users>().HasData(new Users
            {
                Id = 2,
                Code = "US-2",
                UserName = "Admin",
                Password = "iskJ2vxZtjfyhzajgb3HkQ==",
                ImageName = "default.jpg",
                Email = "exm1@gmail.com",
                Mobile = "1235",
                CreationDate = DateTime.Now

            });
            #endregion

            #region system options
            //SystemOptions
            modelBuilder.Entity<SystemOption>().HasData(new SystemOption
            {
                Id = 1,
                Code = "OPT-1",
                CategoryArName = "اعدادات الاجهزة",
                CategoryEnName = "Devices Settings",
                ArabicName = "بداية دوام العمل",
                EnglishName = "Max Devices Count",
                DefaultValue = "5",
                Value = "5",
                Time = new TimeSpan(9, 00, 00),
                ControlType = "startday"
            });
            modelBuilder.Entity<SystemOption>().HasData(new SystemOption
            {
                Id = 2,
                Code = "OPT-2",
                CategoryArName = "اعدادات الاجهزة",
                CategoryEnName = "Devices Settings",
                ArabicName = "نهاية دوام العمل",
                EnglishName = "Max Devices Count",
                DefaultValue = "5",
                Value = "5",
                Time = new TimeSpan(21, 00, 00),
                ControlType = "startday"
            });
            modelBuilder.Entity<SystemOption>().HasData(new SystemOption
            {
                Id = 3,
                Code = "OPT-3",
                CategoryArName = "اعدادات الاجهزة",
                CategoryEnName = "Devices Settings",
                ArabicName = "المسافة بين الطلبات بالدقائق",
                EnglishName = "Max Devices Count",
                DefaultValue = "30",
                Value = "30",
                ControlType = "textbox"
            });
            #endregion


            #region Modules

            modelBuilder.Entity<Modules>().HasData(new Modules
            {
                Id = 1,
                Code = "MOD-1",
                ArabicName = "ادارة المستخدمين",
                EnglishName = "Users Management",
                CreationDate = DateTime.Now,
                Icon = "fas fa-users-cog",
            });

            modelBuilder.Entity<Modules>().HasData(new Modules
            {
                Id = 2,
                Code = "MOD-2",
                ArabicName = "اعدادات النظام",
                EnglishName = "System Settings",
                Icon = "fa fa-cogs",
                CreationDate = DateTime.Now
            });

            modelBuilder.Entity<Modules>().HasData(new Modules
            {
                Id = 3,
                Code = "MOD-3",
                ArabicName = "التقارير",
                EnglishName = "Orders Management",
                Icon = "fas fa-shopping-cart",
                CreationDate = DateTime.Now
            });

            modelBuilder.Entity<Modules>().HasData(new Modules
            {
                Id = 4,
                Code = "MOD-4",
                ArabicName = "ادارة العملاء",
                EnglishName = "Customers Management",
                Icon = "fas fa-user-tie",
                IsDeleted = true,
                CreationDate = DateTime.Now
            });



            modelBuilder.Entity<Modules>().HasData(new Modules
            {
                Id = 6,
                Code = "MOD-6",
                ArabicName = "الاعدادات",
                EnglishName = "Configuration",
                IsDeleted = true,
                Icon = "fa fa-cogs",
                CreationDate = DateTime.Now
            });


            #endregion

            #region Permissions

            // View
            modelBuilder.Entity<Permissions>().HasData(new Permissions
            {
                Id = 1,
                Code = "PER-1",
                ArabicName = "عرض",
                EnglishName = "View",
                PermissionCode = "Index",
                CreationDate = DateTime.Now
            });

            // Create
            modelBuilder.Entity<Permissions>().HasData(new Permissions
            {
                Id = 2,
                Code = "PER-2",
                ArabicName = "أنشاء",
                EnglishName = "Create",
                CreationDate = DateTime.Now
            });

            // Edit
            modelBuilder.Entity<Permissions>().HasData(new Permissions
            {
                Id = 3,
                Code = "PER-3",
                ArabicName = "تعديل",
                EnglishName = "Edit",
                CreationDate = DateTime.Now
            });

            // Delete
            modelBuilder.Entity<Permissions>().HasData(new Permissions
            {
                Id = 4,
                Code = "PER-4",
                ArabicName = "حذف",
                EnglishName = "Delete",
                CreationDate = DateTime.Now
            });

            #endregion

            #region Screens

            // groups
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 1,
                Code = "UGR-1",
                ArabicName = "المجموعات",
                EnglishName = "Groups",
                ScreenCode = "Groups",
                Icon = "fas fa-users",
                CreationDate = DateTime.Now,
                ModuleId = 1
            });

            // users
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 2,
                Code = "UUS-2",
                ArabicName = "المستخدمين",
                EnglishName = "Users",
                ScreenCode = "Users",
                Icon = "fas fa-users-cog",
                CreationDate = DateTime.Now,
                ModuleId = 1
            });

            // Lookups
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 3,
                Code = "SSL-3",
                ArabicName = "المعلومات الثابتة",
                EnglishName = "LookUps",
                ScreenCode = "LookUps",
                Icon = "fas fa-indent",
                CreationDate = DateTime.Now,
                IsDeleted = true,
                ModuleId = 2
            });

            // JobTitle
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 4,
                Code = "JJT-4",
                ArabicName = "المسميات الوظيفية",
                EnglishName = "Job Titles",
                ScreenCode = "JobTitle",
                Icon = "fas fa-user-md",
                CreationDate = DateTime.Now,

                ModuleId = 2
            });


            //OrderTrackAction
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 5,
                Code = "Cat-42",
                ArabicName = "حالات الطلب",
                EnglishName = "OrderTrackAction",
                ScreenCode = "OrderTrackAction",
                Icon = "fas fa-luggage-cart",
                CreationDate = DateTime.Now,
                ModuleId = 4
            });

            // Countries
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 6,
                Code = "CCD-9",
                ArabicName = "الدول ",
                EnglishName = "Countries",
                ScreenCode = "Country",
                Icon = "fas fa-globe-europe",
                CreationDate = DateTime.Now,
                ModuleId = 2
            });


            //Service
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 7,
                Code = "SC-23",
                ArabicName = " الخدمات",
                EnglishName = "Services",
                ScreenCode = "Services",
                Icon = "fas fa-code-branch",
                CreationDate = DateTime.Now,
                ModuleId = 6
            });



            // Map Drawing  
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 8,
                Code = "DR-35",
                ArabicName = "رسم المناطق",
                EnglishName = "Draw Region",
                ScreenCode = "DrawRegion",
                Icon = "fa fa-map-marker",
                CreationDate = DateTime.Now,
                IsDeleted = true,
                ModuleId = 2
            });

            //Items
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 9,
                Code = "Cat-42",
                ArabicName = "قطع الغيار",
                EnglishName = "Items",
                ScreenCode = "Items",
                Icon = "fas fa-code-branch",
                CreationDate = DateTime.Now,
                ModuleId = 6
            });

            //Customer
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 10,
                Code = "Cat-42",
                ArabicName = "العملاء",
                EnglishName = "Customer",
                ScreenCode = "Customer",
                Icon = "fas fa-users",
                CreationDate = DateTime.Now,
                ModuleId = 4
            });



            //offers
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 11,
                Code = "Cat-42",
                ArabicName = "العروض",
                EnglishName = "Offers",
                ScreenCode = "Offers",
                Icon = "fas fa-luggage-cart",
                CreationDate = DateTime.Now,
                ModuleId = 4
            });
            // Services report
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 12,
                Code = "SER-27",
                ArabicName = "الصيانة",
                EnglishName = "Services",
                ScreenCode = "ServicesReport",
                Icon = "fas fa-code-branch",
                CreationDate = DateTime.Now,
                ModuleId = 6
            });

            //Order
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 13,
                Code = "Cat-42",
                ArabicName = "الطلبات",
                EnglishName = "Orders",
                ScreenCode = "Order",
                Icon = "fas fa-luggage-cart",
                CreationDate = DateTime.Now,
                ModuleId = 4
            });
            //Order
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 14,
                Code = "Cat-42",
                ArabicName = "الفنين",
                EnglishName = "Technicals",
                ScreenCode = "TechnicalsReport",
                Icon = "fas fa-users-cog",
                CreationDate = DateTime.Now,
                ModuleId = 4
            });
            //Order
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 15,
                Code = "Cat-42",
                ArabicName = "اعدات النظام",
                EnglishName = "Technicals",
                ScreenCode = "SystemOption",
                Icon = "fas fa-users-cog",
                CreationDate = DateTime.Now,
                ModuleId = 2
            });
            
            modelBuilder.Entity<Screens>().HasData(new Screens
            {
                Id = 16,
                Code = "Cat-16",
                ArabicName = "اراء العملاء",
                EnglishName = "Customer Reviews",
                ScreenCode = "CustomerReview",
                Icon = "fas fa-users-cog",
                CreationDate = DateTime.Now,
                ModuleId = 2
            });



            #endregion

            #region Screens Permissions

            // groups
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 1,
                ScreenId = 1,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 2,
                ScreenId = 1,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 3,
                ScreenId = 1,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 4,
                ScreenId = 1,
                PermissionId = 4
            });

            // Users
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 5,
                ScreenId = 2,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 6,
                ScreenId = 2,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 7,
                ScreenId = 2,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 8,
                ScreenId = 2,
                PermissionId = 4
            });

            // Lookups
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 9,
                ScreenId = 3,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 10,
                ScreenId = 3,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 11,
                ScreenId = 3,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 12,
                ScreenId = 3,
                PermissionId = 4
            });

            // JobTitle
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 13,
                ScreenId = 4,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 14,
                ScreenId = 4,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 15,
                ScreenId = 4,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 16,
                ScreenId = 4,
                PermissionId = 4
            });

            // TaskType
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 21,
                ScreenId = 6,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 22,
                ScreenId = 6,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 23,
                ScreenId = 6,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 24,
                ScreenId = 6,
                PermissionId = 4
            });

            // Countries
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 33,
                ScreenId = 9,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 34,
                ScreenId = 9,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 35,
                ScreenId = 9,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 36,
                ScreenId = 9,
                PermissionId = 4
            });

            // Work Shifts
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 41,
                ScreenId = 11,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 42,
                ScreenId = 11,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 43,
                ScreenId = 11,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 44,
                ScreenId = 11,
                PermissionId = 4
            });


            // Customers
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 49,
                ScreenId = 13,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 50,
                ScreenId = 13,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 51,
                ScreenId = 13,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 52,
                ScreenId = 13,
                PermissionId = 4
            });


            // CompanyBranches => 18
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 66,
                ScreenId = 14,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 67,
                ScreenId = 14,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 68,
                ScreenId = 14,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 69,
                ScreenId = 14,
                PermissionId = 4
            });
            // Model Maker

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 78,
                ScreenId = 15,
                PermissionId = 1
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 79,
                ScreenId = 15,
                PermissionId = 2
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 80,
                ScreenId = 15,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 81,
                ScreenId = 16,
                PermissionId = 4
            });
            





         


            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 84,
                ScreenId = 22,
                PermissionId = 3
            });

            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 85,
                ScreenId = 22,
                PermissionId = 4
            });

            // Vehicles Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 86,
                ScreenId = 23,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 87,
                ScreenId = 23,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 88,
                ScreenId = 23,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 89,
                ScreenId = 23,
                PermissionId = 4
            });

            // VehiclesStatus Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 90,
                ScreenId = 24,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 91,
                ScreenId = 24,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 92,
                ScreenId = 24,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 93,
                ScreenId = 24,
                PermissionId = 4
            });

            // Contracts Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 94,
                ScreenId = 5,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 95,
                ScreenId = 5,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 96,
                ScreenId = 5,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 97,
                ScreenId = 5,
                PermissionId = 4
            });


            // Services Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 98,
                ScreenId = 26,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 99,
                ScreenId = 26,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 100,
                ScreenId = 26,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 101,
                ScreenId = 26,
                PermissionId = 4
            });



            // Odometer Measures Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 110,
                ScreenId = 29,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 111,
                ScreenId = 29,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 112,
                ScreenId = 29,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 113,
                ScreenId = 29,
                PermissionId = 4
            });

            //Costs Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 114,
                ScreenId = 30,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 115,
                ScreenId = 30,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 116,
                ScreenId = 30,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 117,
                ScreenId = 30,
                PermissionId = 4
            });

            //Vehicle Fuel Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 118,
                ScreenId = 31,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 119,
                ScreenId = 31,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 120,
                ScreenId = 31,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 121,
                ScreenId = 31,
                PermissionId = 4
            });

            //Odometer Vehicle Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 122,
                ScreenId = 32,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 123,
                ScreenId = 32,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 124,
                ScreenId = 32,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 125,
                ScreenId = 32,
                PermissionId = 4
            });
            //Odometer Vehicle Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 126,
                ScreenId = 35,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 127,
                ScreenId = 35,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 128,
                ScreenId = 35,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 129,
                ScreenId = 35,
                PermissionId = 4
            });


            //driver Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 130,
                ScreenId = 36,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 131,
                ScreenId = 36,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 132,
                ScreenId = 36,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 133,
                ScreenId = 36,
                PermissionId = 4
            });


            //vehicle driver Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 134,
                ScreenId = 37,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 135,
                ScreenId = 37,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 136,
                ScreenId = 37,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 137,
                ScreenId = 37,
                PermissionId = 4
            });

            //vehicle Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 138,
                ScreenId = 38,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 139,
                ScreenId = 38,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 140,
                ScreenId = 38,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 141,
                ScreenId = 38,
                PermissionId = 4
            });

            //SysKeyVal Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 142,
                ScreenId = 39,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 143,
                ScreenId = 39,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 144,
                ScreenId = 39,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 145,
                ScreenId = 39,
                PermissionId = 4
            });


            //SysKeyVal Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 146,
                ScreenId = 33,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 147,
                ScreenId = 33,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 148,
                ScreenId = 33,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 149,
                ScreenId = 33,
                PermissionId = 4
            });


            //SysKeyVal Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 150,
                ScreenId = 34,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 151,
                ScreenId = 34,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 152,
                ScreenId = 34,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 153,
                ScreenId = 34,
                PermissionId = 4
            });
            //VehicleRegion Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 154,
                ScreenId = 40,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 155,
                ScreenId = 40,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 156,
                ScreenId = 40,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 157,
                ScreenId = 40,
                PermissionId = 4
            });

            //Languages Permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 158,
                ScreenId = 41,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 159,
                ScreenId = 41,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 160,
                ScreenId = 41,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 161,
                ScreenId = 41,
                PermissionId = 4
            });

            // Cameras permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 162,
                ScreenId = 42,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 163,
                ScreenId = 42,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 164,
                ScreenId = 42,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 165,
                ScreenId = 42,
                PermissionId = 4
            });

            // Vehicle Category permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 166,
                ScreenId = 43,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 167,
                ScreenId = 43,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 168,
                ScreenId = 43,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 169,
                ScreenId = 43,
                PermissionId = 4
            });

            // Alerts permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 170,
                ScreenId = 44,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 171,
                ScreenId = 44,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 172,
                ScreenId = 44,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 173,
                ScreenId = 44,
                PermissionId = 4
            });

            // company permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 174,
                ScreenId = 45,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 175,
                ScreenId = 45,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 176,
                ScreenId = 45,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 177,
                ScreenId = 45,
                PermissionId = 4
            });
            // Maintenance permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 178,
                ScreenId = 46,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 179,
                ScreenId = 46,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 180,
                ScreenId = 46,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 181,
                ScreenId = 46,
                PermissionId = 4
            });
            // Decument Driver permissions
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 182,
                ScreenId = 47,
                PermissionId = 1
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 183,
                ScreenId = 47,
                PermissionId = 2
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 184,
                ScreenId = 47,
                PermissionId = 3
            });
            modelBuilder.Entity<ScreenPermissions>().HasData(new ScreenPermissions
            {
                Id = 185,
                ScreenId = 47,
                PermissionId = 4
            });
            #endregion


            #region Job title
            modelBuilder.Entity<JobTitle>().HasData(new JobTitle
            {
                Id = 1,
                Code = "Job-1",
                ArabicName = "مدير",
                EnglishName = "Manager",
                CreationDate = DateTime.Now
            });

            modelBuilder.Entity<JobTitle>().HasData(new JobTitle
            {
                Id = 2,
                Code = "Job-2",
                ArabicName = "فني",
                EnglishName = "Technical",
                CreationDate = DateTime.Now
            });
            modelBuilder.Entity<JobTitle>().HasData(new JobTitle
            {
                Id = 3,
                Code = "Job-3",
                ArabicName = "مورد",
                EnglishName = "Supplier",
                CreationDate = DateTime.Now
            });
            modelBuilder.Entity<JobTitle>().HasData(new JobTitle
            {
                Id = 4,
                Code = "Job-4",
                ArabicName = "سكرتيره",
                EnglishName = "Secretary",
                CreationDate = DateTime.Now
            });
            #endregion

            #region OrderAction
            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 1,
                Code = "or-action-1",
                ArabicName = "ملغى",
                EnglishName = "canceled",
                CreationDate = DateTime.Now,
            });
            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 2,
                Code = "or-action-2",
                ArabicName = "مرفوض",
                EnglishName = "rejected",
                CreationDate = DateTime.Now,
            });
            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 3,
                Code = "or-action-3",
                ArabicName = "طلب",
                EnglishName = "Ordered",
                CreationDate = DateTime.Now,
            });
            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 4,
                Code = "or-action-4",
                ArabicName = "استلمها المستخدم ",
                EnglishName = "accpted_by_user",
                CreationDate = DateTime.Now,
            });
            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 5,
                Code = "or-action-5",
                ArabicName = "في تقدم",
                EnglishName = "in_progress",
                CreationDate = DateTime.Now,
            });


            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 6,
                Code = "or-action-6",
                ArabicName = "شحنت",
                EnglishName = "shipped",
                CreationDate = DateTime.Now,
            });

            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 7,
                Code = "or-action-7",
                ArabicName = "تم التوصيل",
                EnglishName = "delivered",
                CreationDate = DateTime.Now,
            });


            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 8,
                Code = "or-action-8",
                ArabicName = "منتهى",
                EnglishName = "completed",
                CreationDate = DateTime.Now,
            });


            modelBuilder.Entity<OrderTrackAction>().HasData(new OrderTrackAction
            {
                Id = 9,
                Code = "or-action-9",
                ArabicName = "تم تقيمه",
                EnglishName = "reviewed",
                CreationDate = DateTime.Now,
            });

            #endregion
            #endregion

            base.OnModelCreating(modelBuilder);


        }




        public virtual DbSet<ForgetPasswordURL> ForgetPasswordURLs { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<GroupPermissions> GroupPermissions { get; set; }
        public virtual DbSet<Screens> Screens { get; set; }
        public virtual DbSet<Modules> Modules { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<District> District { get; set; }

        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RegionPoints> RegionPoints { get; set; }

        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<PocketHistory> PocketHistories { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UserGroups> UserGroups { get; set; }
        public virtual DbSet<Technical> Technicals { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemImage> ItemImages { get; set; }

        public virtual DbSet<Offer> Offers { get; set; }
        public virtual DbSet<OfferActiveDate> OfferActiveDate { get; set; }
        public virtual DbSet<OfferItem> OfferItem { get; set; }

        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<ItemFavourite> ItemFavourite { get; set; }
        public virtual DbSet<ItemsCart> ItemsCart { get; set; }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItems> OrderItems { get; set; }
        public virtual DbSet<OrderServices> OrderServices { get; set; }
        public virtual DbSet<OrderTrackAction> OrderTrackAction { get; set; }
        public virtual DbSet<OrderTrack> OrderTrack { get; set; }
        public virtual DbSet<ReadyTechnicals> ReadyTechnicals { get; set; }
        public virtual DbSet<OrderTechnicalAssignment> OrderTechnicalAssignment { get; set; }
        public virtual DbSet<ContactUs> ContactUs { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<OrderServicesImages> ServicesImages { get; set; }
        public virtual DbSet<SystemOption> SystemOption { get; set; }
        public virtual DbSet<CustomerReview> CustomerReview { get; set; }


    }
}
