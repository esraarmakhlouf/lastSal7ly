using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class dfjgjjfg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTechnicalAssignment_Users_TechnicalId",
                table: "OrderTechnicalAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicals_Services_ServiceId",
                table: "Technicals");

            migrationBuilder.DropIndex(
                name: "IX_Users_Mobile",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.RenameColumn(
                name: "TechnicalId",
                table: "OrderTechnicalAssignment",
                newName: "TechnicalUserId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTechnicalAssignment_TechnicalId",
                table: "OrderTechnicalAssignment",
                newName: "IX_OrderTechnicalAssignment_TechnicalUserId");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Technicals",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerReview",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ImagePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReview", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 216, DateTimeKind.Local).AddTicks(3859));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 228, DateTimeKind.Local).AddTicks(2693));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 228, DateTimeKind.Local).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 228, DateTimeKind.Local).AddTicks(5933));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 228, DateTimeKind.Local).AddTicks(5970));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(2857));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(4885));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ArabicName", "CreationDate" },
                values: new object[] { "التقارير", new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(4960) });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDate", "IsDeleted" },
                values: new object[] { new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(6082), true });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreationDate", "IsDeleted" },
                values: new object[] { new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(6213), true });

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(1922));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2657));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2712));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2734));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2753));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2783));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2802));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2821));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 229, DateTimeKind.Local).AddTicks(2839));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 225, DateTimeKind.Local).AddTicks(4829));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 225, DateTimeKind.Local).AddTicks(5473));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 225, DateTimeKind.Local).AddTicks(5503));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 225, DateTimeKind.Local).AddTicks(5524));

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 66L,
                column: "ScreenId",
                value: 14L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 67L,
                column: "ScreenId",
                value: 14L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 68L,
                column: "ScreenId",
                value: 14L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 69L,
                column: "ScreenId",
                value: 14L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 78L,
                column: "ScreenId",
                value: 15L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 79L,
                column: "ScreenId",
                value: 15L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 80L,
                column: "ScreenId",
                value: 15L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 81L,
                column: "ScreenId",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 94L,
                column: "ScreenId",
                value: 5L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 95L,
                column: "ScreenId",
                value: 5L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 96L,
                column: "ScreenId",
                value: 5L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 97L,
                column: "ScreenId",
                value: 5L);

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1428));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1461));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1483));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1505));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1549));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1583));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1602));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1624));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1643));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1662));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1681));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1699));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1718));

            migrationBuilder.InsertData(
                table: "Screens",
                columns: new[] { "Id", "ArabicName", "Code", "CreatedBy", "CreationDate", "EnglishName", "Icon", "IsActive", "IsDeleted", "ModificationDate", "ModifiedBy", "ModuleId", "Rank", "ScreenCode", "URL" },
                values: new object[] { 16L, "اراء العملاء", "Cat-16", null, new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1737), "Customer Reviews", "fas fa-users-cog", true, false, null, null, 2L, 1, "CustomerReview", null });

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 218, DateTimeKind.Local).AddTicks(2781));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "Time" },
                values: new object[] { new DateTime(2020, 8, 15, 10, 6, 7, 219, DateTimeKind.Local).AddTicks(2244), new TimeSpan(0, 21, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 219, DateTimeKind.Local).AddTicks(2322));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "Mobile" },
                values: new object[] { new DateTime(2020, 8, 15, 10, 6, 7, 217, DateTimeKind.Local).AddTicks(7937), "1234" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "Email", "Mobile" },
                values: new object[] { new DateTime(2020, 8, 15, 10, 6, 7, 217, DateTimeKind.Local).AddTicks(9792), "exm1@gmail.com", "1235" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Mobile",
                table: "Users",
                column: "Mobile",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTechnicalAssignment_Users_TechnicalUserId",
                table: "OrderTechnicalAssignment",
                column: "TechnicalUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technicals_Services_ServiceId",
                table: "Technicals",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderTechnicalAssignment_Users_TechnicalUserId",
                table: "OrderTechnicalAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Technicals_Services_ServiceId",
                table: "Technicals");

            migrationBuilder.DropTable(
                name: "CustomerReview");

            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Mobile",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DeleteData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.RenameColumn(
                name: "TechnicalUserId",
                table: "OrderTechnicalAssignment",
                newName: "TechnicalId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderTechnicalAssignment_TechnicalUserId",
                table: "OrderTechnicalAssignment",
                newName: "IX_OrderTechnicalAssignment_TechnicalId");

            migrationBuilder.AlterColumn<string>(
                name: "Mobile",
                table: "Users",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Technicals",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 185, DateTimeKind.Local).AddTicks(9627));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 200, DateTimeKind.Local).AddTicks(9355));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 200, DateTimeKind.Local).AddTicks(9863));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 200, DateTimeKind.Local).AddTicks(9914));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 200, DateTimeKind.Local).AddTicks(9958));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 188, DateTimeKind.Local).AddTicks(8844));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 189, DateTimeKind.Local).AddTicks(246));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "ArabicName", "CreationDate" },
                values: new object[] { "ادارة الطلبات", new DateTime(2020, 8, 6, 16, 27, 35, 189, DateTimeKind.Local).AddTicks(314) });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreationDate", "IsDeleted" },
                values: new object[] { new DateTime(2020, 8, 6, 16, 27, 35, 189, DateTimeKind.Local).AddTicks(1326), false });

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "CreationDate", "IsDeleted" },
                values: new object[] { new DateTime(2020, 8, 6, 16, 27, 35, 189, DateTimeKind.Local).AddTicks(1436), false });

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4099));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4539));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4592));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4635));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4675));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4720));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4761));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4803));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 201, DateTimeKind.Local).AddTicks(4936));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 196, DateTimeKind.Local).AddTicks(8437));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 196, DateTimeKind.Local).AddTicks(9345));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 196, DateTimeKind.Local).AddTicks(9414));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 196, DateTimeKind.Local).AddTicks(9469));

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 66L,
                column: "ScreenId",
                value: 18L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 67L,
                column: "ScreenId",
                value: 18L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 68L,
                column: "ScreenId",
                value: 18L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 69L,
                column: "ScreenId",
                value: 18L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 78L,
                column: "ScreenId",
                value: 21L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 79L,
                column: "ScreenId",
                value: 21L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 80L,
                column: "ScreenId",
                value: 21L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 81L,
                column: "ScreenId",
                value: 21L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 94L,
                column: "ScreenId",
                value: 25L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 95L,
                column: "ScreenId",
                value: 25L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 96L,
                column: "ScreenId",
                value: 25L);

            migrationBuilder.UpdateData(
                table: "ScreenPermissions",
                keyColumn: "Id",
                keyValue: 97L,
                column: "ScreenId",
                value: 25L);

            migrationBuilder.InsertData(
                table: "ScreenPermissions",
                columns: new[] { "Id", "PermissionId", "ScreenId" },
                values: new object[,]
                {
                    { 82L, 1L, 22L },
                    { 83L, 2L, 22L },
                    { 74L, 1L, 20L },
                    { 75L, 2L, 20L },
                    { 76L, 3L, 20L },
                    { 77L, 4L, 20L }
                });

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(5692));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7105));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7177));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7252));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7304));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7367));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7414));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7465));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7539));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7646));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7694));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7745));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7797));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 197, DateTimeKind.Local).AddTicks(7846));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 187, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "Time" },
                values: new object[] { new DateTime(2020, 8, 6, 16, 27, 35, 188, DateTimeKind.Local).AddTicks(4644), new TimeSpan(0, 9, 0, 0, 0) });

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 188, DateTimeKind.Local).AddTicks(4775));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "Mobile" },
                values: new object[] { new DateTime(2020, 8, 6, 16, 27, 35, 187, DateTimeKind.Local).AddTicks(2346), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "Email", "Mobile" },
                values: new object[] { new DateTime(2020, 8, 6, 16, 27, 35, 187, DateTimeKind.Local).AddTicks(3927), "exm@gmail.com", "845647" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Mobile",
                table: "Users",
                column: "Mobile",
                unique: true,
                filter: "[Mobile] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderTechnicalAssignment_Users_TechnicalId",
                table: "OrderTechnicalAssignment",
                column: "TechnicalId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Technicals_Services_ServiceId",
                table: "Technicals",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
