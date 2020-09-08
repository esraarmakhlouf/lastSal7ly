using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class sal7ly2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 950, DateTimeKind.Local).AddTicks(1065));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 963, DateTimeKind.Local).AddTicks(6869));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 963, DateTimeKind.Local).AddTicks(7293));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 963, DateTimeKind.Local).AddTicks(7319));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 963, DateTimeKind.Local).AddTicks(7335));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 952, DateTimeKind.Local).AddTicks(456));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 952, DateTimeKind.Local).AddTicks(2492));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 952, DateTimeKind.Local).AddTicks(2578));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 952, DateTimeKind.Local).AddTicks(3695));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 952, DateTimeKind.Local).AddTicks(3884));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 963, DateTimeKind.Local).AddTicks(9747));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(162));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(188));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(207));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(223));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(243));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(258));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(275));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 964, DateTimeKind.Local).AddTicks(291));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 960, DateTimeKind.Local).AddTicks(7526));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 960, DateTimeKind.Local).AddTicks(8677));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 960, DateTimeKind.Local).AddTicks(8740));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 960, DateTimeKind.Local).AddTicks(8778));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(3327));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4300));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4335));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4363));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4384));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4408));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4448));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4468));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4490));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4511));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4531));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 23, 7, 961, DateTimeKind.Local).AddTicks(4569));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreationDate", "Email" },
                values: new object[] { new DateTime(2020, 7, 24, 5, 23, 7, 951, DateTimeKind.Local).AddTicks(4031), "asd@yahoo.com" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreationDate", "Email" },
                values: new object[] { new DateTime(2020, 7, 24, 5, 23, 7, 951, DateTimeKind.Local).AddTicks(5802), "asd@yahoo.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 553, DateTimeKind.Local).AddTicks(196));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(380));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(1045));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(1090));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(1121));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(6414));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(7613));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(7663));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(8500));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(8619));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(4748));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5382));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5430));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5464));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5497));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5534));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5584));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 569, DateTimeKind.Local).AddTicks(5652));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 562, DateTimeKind.Local).AddTicks(8451));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 562, DateTimeKind.Local).AddTicks(9914));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 563, DateTimeKind.Local).AddTicks(87));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 563, DateTimeKind.Local).AddTicks(258));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 563, DateTimeKind.Local).AddTicks(8971));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1555));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1745));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1917));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(1983));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2122));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2188));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2250));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2346));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2618));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2718));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2887));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(2979));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 564, DateTimeKind.Local).AddTicks(3066));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(1319));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 20, 18, 49, 6, 554, DateTimeKind.Local).AddTicks(2902));
        }
    }
}
