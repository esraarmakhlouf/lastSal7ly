using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class djfjgjd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Rate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AddColumn<double>(
                name: "TechCommission",
                table: "Orders",
                nullable: false,
                defaultValue: 0.0);

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
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 189, DateTimeKind.Local).AddTicks(314));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 189, DateTimeKind.Local).AddTicks(1326));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 189, DateTimeKind.Local).AddTicks(1436));

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
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 188, DateTimeKind.Local).AddTicks(4644));

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
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 187, DateTimeKind.Local).AddTicks(2346));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 6, 16, 27, 35, 187, DateTimeKind.Local).AddTicks(3927));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechCommission",
                table: "Orders");

            migrationBuilder.AlterColumn<double>(
                name: "Rate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int));

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 709, DateTimeKind.Local).AddTicks(5013));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 1, 19, 40, 36, 709, DateTimeKind.Local).AddTicks(5991));
        }
    }
}
