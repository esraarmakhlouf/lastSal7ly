using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class sal7ly5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemOptions",
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
                    ArabicName = table.Column<string>(maxLength: 200, nullable: false),
                    EnglishName = table.Column<string>(maxLength: 200, nullable: true),
                    CategoryArName = table.Column<string>(maxLength: 200, nullable: false),
                    CategoryEnName = table.Column<string>(maxLength: 200, nullable: true),
                    ControlType = table.Column<string>(maxLength: 200, nullable: true),
                    DropdownOptions = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ArabicValue = table.Column<string>(nullable: true),
                    EnglishValue = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Time = table.Column<TimeSpan>(nullable: true),
                    IsReadOnly = table.Column<bool>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemOptions", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 906, DateTimeKind.Local).AddTicks(5392));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(2415));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(2776));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(2800));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(2824));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 909, DateTimeKind.Local).AddTicks(2074));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 909, DateTimeKind.Local).AddTicks(3277));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 909, DateTimeKind.Local).AddTicks(3331));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 909, DateTimeKind.Local).AddTicks(4070));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 909, DateTimeKind.Local).AddTicks(4166));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(4689));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5036));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5063));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5081));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5099));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5118));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5135));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5152));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 915, DateTimeKind.Local).AddTicks(5168));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(3992));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(4514));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(4546));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(4566));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7074));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7793));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7827));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7857));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7876));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7900));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7954));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7973));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(7993));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(8010));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(8028));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(8046));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.InsertData(
                table: "Screens",
                columns: new[] { "Id", "ArabicName", "Code", "CreatedBy", "CreationDate", "EnglishName", "Icon", "IsActive", "IsDeleted", "ModificationDate", "ModifiedBy", "ModuleId", "Rank", "ScreenCode", "URL" },
                values: new object[] { 15L, "اعدات النظام", "Cat-42", null, new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(8082), "Technicals", "fas fa-users-cog", true, false, null, null, 2L, 1, "SystemOption", null });

            migrationBuilder.InsertData(
                table: "SystemOptions",
                columns: new[] { "Id", "ArabicName", "ArabicValue", "CategoryArName", "CategoryEnName", "Code", "ControlType", "CreatedBy", "CreationDate", "Date", "DefaultValue", "DropdownOptions", "EnglishName", "EnglishValue", "IsActive", "IsDeleted", "IsHidden", "IsReadOnly", "ModificationDate", "ModifiedBy", "Time", "Title", "Value" },
                values: new object[,]
                {
                    { 1L, "بداية دوام العمل", null, "اعدادات الاجهزة", "Devices Settings", "OPT-1", "startday", null, new DateTime(2020, 7, 31, 20, 42, 49, 907, DateTimeKind.Local).AddTicks(9026), null, "5", null, "Max Devices Count", null, true, false, false, false, null, null, new TimeSpan(0, 9, 0, 0, 0), null, "5" },
                    { 2L, "نهاية دوام العمل", null, "اعدادات الاجهزة", "Devices Settings", "OPT-2", "startday", null, new DateTime(2020, 7, 31, 20, 42, 49, 908, DateTimeKind.Local).AddTicks(8968), null, "5", null, "Max Devices Count", null, true, false, false, false, null, null, new TimeSpan(0, 9, 0, 0, 0), null, "5" },
                    { 3L, "المسافة بين الطلبات بالدقائق", null, "اعدادات الاجهزة", "Devices Settings", "OPT-3", "textbox", null, new DateTime(2020, 7, 31, 20, 42, 49, 908, DateTimeKind.Local).AddTicks(9105), null, "30", null, "Max Devices Count", null, true, false, false, false, null, null, null, null, "30" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "Email" },
                values: new object[] { new DateTime(2020, 7, 31, 20, 42, 49, 907, DateTimeKind.Local).AddTicks(6615), "exm@gmail.com" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "Email" },
                values: new object[] { new DateTime(2020, 7, 31, 20, 42, 49, 907, DateTimeKind.Local).AddTicks(7957), "exm@gmail.com" });

            migrationBuilder.CreateIndex(
                name: "IX_ServicesImages_OrderId",
                table: "ServicesImages",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServicesImages_Orders_OrderId",
                table: "ServicesImages",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServicesImages_Orders_OrderId",
                table: "ServicesImages");

            migrationBuilder.DropTable(
                name: "SystemOptions");

            migrationBuilder.DropIndex(
                name: "IX_ServicesImages_OrderId",
                table: "ServicesImages");

            migrationBuilder.DeleteData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 51, 994, DateTimeKind.Local).AddTicks(97));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 37, DateTimeKind.Local).AddTicks(5154));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 37, DateTimeKind.Local).AddTicks(6017));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 37, DateTimeKind.Local).AddTicks(6073));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 37, DateTimeKind.Local).AddTicks(6117));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 24, DateTimeKind.Local).AddTicks(3829));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 24, DateTimeKind.Local).AddTicks(5367));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 24, DateTimeKind.Local).AddTicks(5435));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 24, DateTimeKind.Local).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 24, DateTimeKind.Local).AddTicks(6634));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(1166));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2060));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2118));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2160));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2200));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2246));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2285));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2323));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 38, DateTimeKind.Local).AddTicks(2361));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(949));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(2266));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(2360));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(2416));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(7976));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(9623));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(9703));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(9771));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(9819));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(9874));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 33, DateTimeKind.Local).AddTicks(9966));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 34, DateTimeKind.Local).AddTicks(13));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 34, DateTimeKind.Local).AddTicks(63));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 34, DateTimeKind.Local).AddTicks(108));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 34, DateTimeKind.Local).AddTicks(154));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 34, DateTimeKind.Local).AddTicks(200));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 34, DateTimeKind.Local).AddTicks(246));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "Email" },
                values: new object[] { new DateTime(2020, 7, 27, 9, 27, 52, 23, DateTimeKind.Local).AddTicks(4856), "default.yahoo.com" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "Email" },
                values: new object[] { new DateTime(2020, 7, 27, 9, 27, 52, 23, DateTimeKind.Local).AddTicks(9835), "default.yahoo.com" });
        }
    }
}
