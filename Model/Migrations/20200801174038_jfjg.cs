using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class jfjg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 708, DateTimeKind.Local).AddTicks(8248));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(6469));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(6824));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(6850));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(6868));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 710, DateTimeKind.Local).AddTicks(2492));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 710, DateTimeKind.Local).AddTicks(3187));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 710, DateTimeKind.Local).AddTicks(3216));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 710, DateTimeKind.Local).AddTicks(3649));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 710, DateTimeKind.Local).AddTicks(3717));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(8619));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(8929));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(9020));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(9041));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(9059));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(9079));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(9096));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(9112));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 715, DateTimeKind.Local).AddTicks(9129));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 713, DateTimeKind.Local).AddTicks(6376));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 713, DateTimeKind.Local).AddTicks(6969));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 713, DateTimeKind.Local).AddTicks(6999));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 713, DateTimeKind.Local).AddTicks(7018));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 713, DateTimeKind.Local).AddTicks(9472));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(212));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(245));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(278));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(298));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(319));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(337));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(354));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(373));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(499));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(522));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(541));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(558));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(576));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 714, DateTimeKind.Local).AddTicks(594));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 709, DateTimeKind.Local).AddTicks(6681));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 710, DateTimeKind.Local).AddTicks(451));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 710, DateTimeKind.Local).AddTicks(517));

          }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 913, DateTimeKind.Local).AddTicks(8082));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 907, DateTimeKind.Local).AddTicks(9026));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 908, DateTimeKind.Local).AddTicks(8968));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 31, 20, 42, 49, 908, DateTimeKind.Local).AddTicks(9105));
        }
    }
}
