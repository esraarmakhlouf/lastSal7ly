using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class sal7ly1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactUs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: true),
                    UserName = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    CityId = table.Column<long>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    EmailActivated = table.Column<bool>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    mobToken = table.Column<string>(nullable: true),
                    Pocket = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForgetPasswordURLs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Token = table.Column<Guid>(maxLength: 50, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForgetPasswordURLs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<long>(nullable: false),
                    PermissionId = table.Column<long>(nullable: false),
                    ScreenId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: true),
                    IsMaster = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: true),
                    IsMaster = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: false),
                    Icon = table.Column<string>(nullable: false),
                    Rank = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ToUSer = table.Column<long>(nullable: false),
                    TypeOfUser = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 500, nullable: false),
                    URl = table.Column<string>(nullable: true),
                    Seen = table.Column<bool>(nullable: false),
                    IsAlert = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferActiveDates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OfferId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferActiveDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OfferId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ordertrackaction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordertrackaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: false),
                    PermissionCode = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PocketHistories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ToUSer = table.Column<long>(nullable: false),
                    TypeUser = table.Column<int>(nullable: false),
                    Transaction = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PocketHistories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: true),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: true),
                    IsCycle = table.Column<bool>(nullable: false),
                    Radius = table.Column<double>(nullable: false),
                    CenterLat = table.Column<double>(nullable: false),
                    CenterLong = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegionPoints",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    lat = table.Column<double>(nullable: false),
                    lng = table.Column<double>(nullable: false),
                    RegionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScreenPermissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ScreenId = table.Column<long>(nullable: false),
                    PermissionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: false),
                    ScreenCode = table.Column<string>(maxLength: 500, nullable: false),
                    Icon = table.Column<string>(nullable: false),
                    ModuleId = table.Column<long>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    URL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: false),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServicesImages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagePath = table.Column<string>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicesImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGroups",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GroupId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: true),
                    CountryID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Country_CountryID",
                        column: x => x.CountryID,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: false),
                    DescriptionArabic = table.Column<string>(nullable: false),
                    DescriptionEnglish = table.Column<string>(nullable: false),
                    ServiceId = table.Column<long>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: true),
                    CityID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_City_CityID",
                        column: x => x.CityID,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemFavourite",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemFavourite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemFavourite_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemFavourite_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemImages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagePath = table.Column<string>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemImages_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemsCart",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsCart_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: false),
                    OfferValue = table.Column<double>(nullable: false),
                    MainCategoryId = table.Column<long>(nullable: false),
                    MainItemId = table.Column<long>(nullable: true),
                    FreeItemId = table.Column<long>(nullable: true),
                    MainQty = table.Column<double>(nullable: false),
                    FreeQty = table.Column<double>(nullable: false),
                    ApplyThroughCoupon = table.Column<bool>(nullable: false),
                    CouponCode = table.Column<string>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offers_Items_FreeItemId",
                        column: x => x.FreeItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offers_Services_MainCategoryId",
                        column: x => x.MainCategoryId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Offers_Items_MainItemId",
                        column: x => x.MainItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ArabicName = table.Column<string>(maxLength: 500, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 500, nullable: false),
                    UserName = table.Column<string>(maxLength: 200, nullable: false),
                    Password = table.Column<string>(maxLength: 200, nullable: false),
                    Mobile = table.Column<string>(nullable: true),
                    JobTitleId = table.Column<long>(nullable: true),
                    CityId = table.Column<long>(nullable: true),
                    DistrictId = table.Column<long>(nullable: true),
                    IsManager = table.Column<bool>(nullable: false),
                    Location = table.Column<string>(nullable: true),
                    IsMaster = table.Column<bool>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Long = table.Column<double>(nullable: false),
                    OnLine = table.Column<bool>(nullable: false),
                    IsCompanyManager = table.Column<bool>(nullable: false),
                    ImageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_JobTitles_JobTitleId",
                        column: x => x.JobTitleId,
                        principalTable: "JobTitles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReadyTechnicals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    TechnicalId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadyTechnicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadyTechnicals_Users_TechnicalId",
                        column: x => x.TechnicalId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technicals",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsersId = table.Column<long>(nullable: false),
                    Pocket = table.Column<double>(nullable: false),
                    ServiceId = table.Column<long>(nullable: true),
                    Commission = table.Column<double>(nullable: false),
                    IsParentage = table.Column<bool>(nullable: false),
                    LastAssignTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technicals_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Technicals_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<long>(nullable: false),
                    IsOrder = table.Column<bool>(nullable: false),
                    Rate = table.Column<double>(nullable: false),
                    Review = table.Column<string>(nullable: true),
                    ResponsibleUserId = table.Column<long>(nullable: true),
                    DeliverDate = table.Column<DateTime>(nullable: true),
                    OrderTrackActionId = table.Column<int>(nullable: false),
                    TechnicalId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Ordertrackaction_OrderTrackActionId",
                        column: x => x.OrderTrackActionId,
                        principalTable: "Ordertrackaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_ResponsibleUserId",
                        column: x => x.ResponsibleUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Technicals_TechnicalId",
                        column: x => x.TechnicalId,
                        principalTable: "Technicals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    DiscountPrice = table.Column<double>(nullable: false),
                    Review = table.Column<string>(maxLength: 500, nullable: true),
                    Rate = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderServices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedBy = table.Column<long>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<long>(nullable: true),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderId = table.Column<long>(nullable: false),
                    ServiceId = table.Column<long>(nullable: false),
                    ServiceImage = table.Column<string>(nullable: true),
                    ServiceComment = table.Column<string>(nullable: true),
                    Review = table.Column<string>(maxLength: 500, nullable: true),
                    Rate = table.Column<int>(nullable: true),
                    IsPriority = table.Column<bool>(nullable: false),
                    DeliverTimeFrom = table.Column<DateTime>(nullable: false),
                    DeliverTimeTo = table.Column<DateTime>(nullable: false),
                    PromoCode = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: false),
                    DiscounttPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderServices_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTechnicalAssignment",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: true),
                    TechnicalId = table.Column<long>(nullable: true),
                    OrderId = table.Column<long>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTechnicalAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTechnicalAssignment_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTechnicalAssignment_Users_TechnicalId",
                        column: x => x.TechnicalId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderTrack",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<long>(nullable: true),
                    OrderId = table.Column<long>(nullable: false),
                    OrderTrackActionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTrack", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTrack_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTrack_Ordertrackaction_OrderTrackActionId",
                        column: x => x.OrderTrackActionId,
                        principalTable: "Ordertrackaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "ArabicName", "Code", "CompanyId", "CreatedBy", "CreationDate", "EnglishName", "IsActive", "IsDeleted", "IsMaster", "ModificationDate", "ModifiedBy" },
                values: new object[] { 1L, "مجموعة عامة", "GR-1", 0L, null, new DateTime(2020, 7, 20, 18, 49, 6, 553, DateTimeKind.Local).AddTicks(196), "General Group", true, false, true, null, null });

            migrationBuilder.InsertData(
                table: "JobTitles",
                columns: new[] { "Id", "ArabicName", "Code", "CreatedBy", "CreationDate", "EnglishName", "IsActive", "IsDeleted", "IsMaster", "ModificationDate", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, "مدير", "Job-1", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(380), "Manager", true, false, false, null, null },
                    { 2L, "فني", "Job-2", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(1045), "Technical", true, false, false, null, null },
                    { 3L, "مورد", "Job-3", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(1090), "Supplier", true, false, false, null, null },
                    { 4L, "سكرتيره", "Job-4", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(1121), "Secretary", true, false, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "Id", "ArabicName", "Code", "CreatedBy", "CreationDate", "EnglishName", "Icon", "IsActive", "IsDeleted", "ModificationDate", "ModifiedBy", "Rank" },
                values: new object[,]
                {
                    { 1L, "ادارة المستخدمين", "MOD-1", null, new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(6414), "Users Management", "fas fa-users-cog", true, false, null, null, 1 },
                    { 2L, "اعدادات النظام", "MOD-2", null, new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(7613), "System Settings", "fa fa-cogs", true, false, null, null, 1 },
                    { 3L, "ادارة الطلبات", "MOD-3", null, new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(7663), "Orders Management", "fas fa-shopping-cart", true, false, null, null, 1 },
                    { 4L, "ادارة العملاء", "MOD-4", null, new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(8500), "Customers Management", "fas fa-user-tie", true, false, null, null, 1 },
                    { 6L, "الاعدادات", "MOD-6", null, new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(8619), "Configuration", "fa fa-cogs", true, false, null, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "Ordertrackaction",
                columns: new[] { "Id", "ArabicName", "Code", "CreatedBy", "CreationDate", "EnglishName", "IsActive", "IsDeleted", "ModificationDate", "ModifiedBy" },
                values: new object[,]
                {
                    { 9, "تم تقيمه", "or-action-9", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5652), "reviewed", true, false, null, null },
                    { 8, "منتهى", "or-action-8", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5618), "completed", true, false, null, null },
                    { 7, "تم التوصيل", "or-action-7", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5584), "delivered", true, false, null, null },
                    { 6, "شحنت", "or-action-6", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5534), "shipped", true, false, null, null },
                    { 1, "ملغى", "or-action-1", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(4748), "canceled", true, false, null, null },
                    { 4, "استلمها المستخدم ", "or-action-4", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5464), "accpted_by_user", true, false, null, null },
                    { 3, "طلب", "or-action-3", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5430), "Ordered", true, false, null, null },
                    { 2, "مرفوض", "or-action-2", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5382), "rejected", true, false, null, null },
                    { 5, "في تقدم", "or-action-5", null, new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5497), "in_progress", true, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ArabicName", "Code", "CreatedBy", "CreationDate", "EnglishName", "IsActive", "IsDeleted", "ModificationDate", "ModifiedBy", "PermissionCode" },
                values: new object[,]
                {
                    { 4L, "حذف", "PER-4", null, new DateTime(2020, 7, 20, 18, 49, 6, 563, DateTimeKind.Local).AddTicks(258), "Delete", true, false, null, null, null },
                    { 3L, "تعديل", "PER-3", null, new DateTime(2020, 7, 20, 18, 49, 6, 563, DateTimeKind.Local).AddTicks(87), "Edit", true, false, null, null, null },
                    { 1L, "عرض", "PER-1", null, new DateTime(2020, 7, 20, 18, 49, 6, 562, DateTimeKind.Local).AddTicks(8451), "View", true, false, null, null, "Index" },
                    { 2L, "أنشاء", "PER-2", null, new DateTime(2020, 7, 20, 18, 49, 6, 562, DateTimeKind.Local).AddTicks(9914), "Create", true, false, null, null, null }
                });

            migrationBuilder.InsertData(
                table: "ScreenPermissions",
                columns: new[] { "Id", "PermissionId", "ScreenId" },
                values: new object[,]
                {
                    { 135L, 2L, 37L },
                    { 136L, 3L, 37L },
                    { 137L, 4L, 37L },
                    { 138L, 1L, 38L },
                    { 139L, 2L, 38L },
                    { 140L, 3L, 38L },
                    { 141L, 4L, 38L },
                    { 142L, 1L, 39L },
                    { 144L, 3L, 39L },
                    { 145L, 4L, 39L },
                    { 146L, 1L, 33L },
                    { 147L, 2L, 33L },
                    { 148L, 3L, 33L },
                    { 149L, 4L, 33L },
                    { 143L, 2L, 39L },
                    { 134L, 1L, 37L },
                    { 123L, 2L, 32L },
                    { 132L, 3L, 36L },
                    { 118L, 1L, 31L },
                    { 119L, 2L, 31L },
                    { 120L, 3L, 31L },
                    { 121L, 4L, 31L },
                    { 122L, 1L, 32L },
                    { 150L, 1L, 34L },
                    { 133L, 4L, 36L },
                    { 124L, 3L, 32L },
                    { 126L, 1L, 35L },
                    { 127L, 2L, 35L },
                    { 128L, 3L, 35L },
                    { 129L, 4L, 35L },
                    { 130L, 1L, 36L },
                    { 131L, 2L, 36L },
                    { 125L, 4L, 32L },
                    { 151L, 2L, 34L },
                    { 162L, 1L, 42L },
                    { 153L, 4L, 34L },
                    { 172L, 3L, 44L },
                    { 173L, 4L, 44L },
                    { 174L, 1L, 45L },
                    { 175L, 2L, 45L },
                    { 176L, 3L, 45L },
                    { 177L, 4L, 45L },
                    { 171L, 2L, 44L },
                    { 178L, 1L, 46L },
                    { 180L, 3L, 46L },
                    { 181L, 4L, 46L },
                    { 182L, 1L, 47L },
                    { 183L, 2L, 47L },
                    { 184L, 3L, 47L },
                    { 185L, 4L, 47L },
                    { 179L, 2L, 46L },
                    { 152L, 3L, 34L },
                    { 170L, 1L, 44L },
                    { 168L, 3L, 43L },
                    { 154L, 1L, 40L },
                    { 155L, 2L, 40L },
                    { 156L, 3L, 40L },
                    { 157L, 4L, 40L },
                    { 158L, 1L, 41L },
                    { 159L, 2L, 41L },
                    { 169L, 4L, 43L },
                    { 160L, 3L, 41L },
                    { 117L, 4L, 30L },
                    { 163L, 2L, 42L },
                    { 164L, 3L, 42L },
                    { 165L, 4L, 42L },
                    { 166L, 1L, 43L },
                    { 167L, 2L, 43L },
                    { 161L, 4L, 41L },
                    { 116L, 3L, 30L },
                    { 112L, 3L, 29L },
                    { 114L, 1L, 30L },
                    { 23L, 3L, 6L },
                    { 24L, 4L, 6L },
                    { 33L, 1L, 9L },
                    { 34L, 2L, 9L },
                    { 35L, 3L, 9L },
                    { 36L, 4L, 9L },
                    { 22L, 2L, 6L },
                    { 41L, 1L, 11L },
                    { 115L, 2L, 30L },
                    { 44L, 4L, 11L },
                    { 49L, 1L, 13L },
                    { 50L, 2L, 13L },
                    { 51L, 3L, 13L },
                    { 52L, 4L, 13L },
                    { 42L, 2L, 11L },
                    { 66L, 1L, 18L },
                    { 21L, 1L, 6L },
                    { 15L, 3L, 4L },
                    { 1L, 1L, 1L },
                    { 2L, 2L, 1L },
                    { 3L, 3L, 1L },
                    { 4L, 4L, 1L },
                    { 5L, 1L, 2L },
                    { 6L, 2L, 2L },
                    { 16L, 4L, 4L },
                    { 7L, 3L, 2L },
                    { 9L, 1L, 3L },
                    { 10L, 2L, 3L },
                    { 11L, 3L, 3L },
                    { 12L, 4L, 3L },
                    { 13L, 1L, 4L },
                    { 14L, 2L, 4L },
                    { 8L, 4L, 2L },
                    { 67L, 2L, 18L },
                    { 43L, 3L, 11L },
                    { 69L, 4L, 18L },
                    { 113L, 4L, 29L },
                    { 111L, 2L, 29L },
                    { 110L, 1L, 29L },
                    { 101L, 4L, 26L },
                    { 100L, 3L, 26L },
                    { 99L, 2L, 26L },
                    { 98L, 1L, 26L },
                    { 68L, 3L, 18L },
                    { 96L, 3L, 25L },
                    { 95L, 2L, 25L },
                    { 94L, 1L, 25L },
                    { 93L, 4L, 24L },
                    { 92L, 3L, 24L },
                    { 91L, 2L, 24L },
                    { 90L, 1L, 24L },
                    { 97L, 4L, 25L },
                    { 88L, 3L, 23L },
                    { 89L, 4L, 23L },
                    { 79L, 2L, 21L },
                    { 80L, 3L, 21L },
                    { 81L, 4L, 21L },
                    { 82L, 1L, 22L },
                    { 83L, 2L, 22L },
                    { 74L, 1L, 20L },
                    { 78L, 1L, 21L },
                    { 76L, 3L, 20L },
                    { 77L, 4L, 20L },
                    { 84L, 3L, 22L },
                    { 85L, 4L, 22L },
                    { 86L, 1L, 23L },
                    { 87L, 2L, 23L },
                    { 75L, 2L, 20L }
                });

            migrationBuilder.InsertData(
                table: "Screens",
                columns: new[] { "Id", "ArabicName", "Code", "CreatedBy", "CreationDate", "EnglishName", "Icon", "IsActive", "IsDeleted", "ModificationDate", "ModifiedBy", "ModuleId", "Rank", "ScreenCode", "URL" },
                values: new object[,]
                {
                    { 14L, "الفنين", "Cat-42", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(3066), "Technicals", "fas fa-users-cog", true, false, null, null, 4L, 1, "TechnicalsReport", null },
                    { 13L, "الطلبات", "Cat-42", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2979), "Orders", "fas fa-luggage-cart", true, false, null, null, 4L, 1, "Order", null },
                    { 12L, "الصيانة", "SER-27", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2887), "Services", "fas fa-code-branch", true, false, null, null, 6L, 1, "ServicesReport", null },
                    { 11L, "العروض", "Cat-42", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2718), "Offers", "fas fa-luggage-cart", true, false, null, null, 4L, 1, "Offers", null },
                    { 10L, "العملاء", "Cat-42", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2618), "Customer", "fas fa-users", true, false, null, null, 4L, 1, "Customer", null },
                    { 9L, "قطع الغيار", "Cat-42", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2346), "Items", "fas fa-code-branch", true, false, null, null, 6L, 1, "Items", null },
                    { 8L, "رسم المناطق", "DR-35", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2250), "Draw Region", "fa fa-map-marker", true, true, null, null, 2L, 1, "DrawRegion", null },
                    { 5L, "حالات الطلب", "Cat-42", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1983), "OrderTrackAction", "fas fa-luggage-cart", true, false, null, null, 4L, 1, "OrderTrackAction", null },
                    { 6L, "الدول ", "CCD-9", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2122), "Countries", "fas fa-globe-europe", true, false, null, null, 2L, 1, "Country", null },
                    { 4L, "المسميات الوظيفية", "JJT-4", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1917), "Job Titles", "fas fa-user-md", true, false, null, null, 2L, 1, "JobTitle", null },
                    { 3L, "المعلومات الثابتة", "SSL-3", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1745), "LookUps", "fas fa-indent", true, true, null, null, 2L, 1, "LookUps", null },
                    { 2L, "المستخدمين", "UUS-2", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1555), "Users", "fas fa-users-cog", true, false, null, null, 1L, 1, "Users", null },
                    { 1L, "المجموعات", "UGR-1", null, new DateTime(2020, 7, 20, 18, 49, 6, 563, DateTimeKind.Local).AddTicks(8971), "Groups", "fas fa-users", true, false, null, null, 1L, 1, "Groups", null },
                    { 7L, " الخدمات", "SC-23", null, new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2188), "Services", "fas fa-code-branch", true, false, null, null, 6L, 1, "Services", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ArabicName", "CityId", "Code", "CreatedBy", "CreationDate", "DistrictId", "EnglishName", "ImageName", "IsActive", "IsCompanyManager", "IsDeleted", "IsManager", "IsMaster", "JobTitleId", "Lat", "Location", "Long", "Mobile", "ModificationDate", "ModifiedBy", "OnLine", "Password", "Token", "UserName" },
                values: new object[,]
                {
                    { 1L, "", null, "US-1", null, new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(1319), null, "", null, true, false, false, false, true, null, 0.0, null, 0.0, null, null, null, false, "4VCsPmt2m2ChrhC2k/i+hw==", null, "AdminDev" },
                    { 2L, "", null, "US-2", null, new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(2902), null, "", null, true, false, false, false, false, null, 0.0, null, 0.0, null, null, null, false, "iskJ2vxZtjfyhzajgb3HkQ==", null, "Admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CountryID",
                table: "City",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Email",
                table: "Customer",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customer_Mobile",
                table: "Customer",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_District_CityID",
                table: "District",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemFavourite_CustomerId",
                table: "ItemFavourite",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemFavourite_ItemId",
                table: "ItemFavourite",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemImages_ItemId",
                table: "ItemImages",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ServiceId",
                table: "Items",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsCart_ItemId",
                table: "ItemsCart",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_FreeItemId",
                table: "Offers",
                column: "FreeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_MainCategoryId",
                table: "Offers",
                column: "MainCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_MainItemId",
                table: "Offers",
                column: "MainItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemId",
                table: "OrderItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTrackActionId",
                table: "Orders",
                column: "OrderTrackActionId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ResponsibleUserId",
                table: "Orders",
                column: "ResponsibleUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TechnicalId",
                table: "Orders",
                column: "TechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_OrderId",
                table: "OrderServices",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderServices_ServiceId",
                table: "OrderServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTechnicalAssignment_OrderId",
                table: "OrderTechnicalAssignment",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTechnicalAssignment_TechnicalId",
                table: "OrderTechnicalAssignment",
                column: "TechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrack_OrderId",
                table: "OrderTrack",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTrack_OrderTrackActionId",
                table: "OrderTrack",
                column: "OrderTrackActionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadyTechnicals_TechnicalId",
                table: "ReadyTechnicals",
                column: "TechnicalId");

            migrationBuilder.CreateIndex(
                name: "IX_Technicals_ServiceId",
                table: "Technicals",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Technicals_UsersId",
                table: "Technicals",
                column: "UsersId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DistrictId",
                table: "Users",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_JobTitleId",
                table: "Users",
                column: "JobTitleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Mobile",
                table: "Users",
                column: "Mobile",
                unique: true,
                filter: "[Mobile] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactUs");

            migrationBuilder.DropTable(
                name: "ForgetPasswordURLs");

            migrationBuilder.DropTable(
                name: "GroupPermissions");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "ItemFavourite");

            migrationBuilder.DropTable(
                name: "ItemImages");

            migrationBuilder.DropTable(
                name: "ItemsCart");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OfferActiveDates");

            migrationBuilder.DropTable(
                name: "OfferItems");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "OrderServices");

            migrationBuilder.DropTable(
                name: "OrderTechnicalAssignment");

            migrationBuilder.DropTable(
                name: "OrderTrack");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PocketHistories");

            migrationBuilder.DropTable(
                name: "ReadyTechnicals");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "RegionPoints");

            migrationBuilder.DropTable(
                name: "ScreenPermissions");

            migrationBuilder.DropTable(
                name: "Screens");

            migrationBuilder.DropTable(
                name: "ServicesImages");

            migrationBuilder.DropTable(
                name: "UserGroups");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Ordertrackaction");

            migrationBuilder.DropTable(
                name: "Technicals");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Country");
        }
    }
}
