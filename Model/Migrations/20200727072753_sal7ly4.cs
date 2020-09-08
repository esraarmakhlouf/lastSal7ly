using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class sal7ly4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "ForgetPasswordURLs",
                newName: "ToId");

            migrationBuilder.AddColumn<int>(
                name: "ToType",
                table: "ForgetPasswordURLs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                table: "Customer",
                nullable: true,
                oldClrType: typeof(long));

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
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 23, DateTimeKind.Local).AddTicks(4856));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 27, 9, 27, 52, 23, DateTimeKind.Local).AddTicks(9835));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_CityId",
                table: "Customer",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customer_City_CityId",
                table: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Customer_CityId",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ToType",
                table: "ForgetPasswordURLs");

            migrationBuilder.RenameColumn(
                name: "ToId",
                table: "ForgetPasswordURLs",
                newName: "CustomerId");

            migrationBuilder.AlterColumn<long>(
                name: "CityId",
                table: "Customer",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 666, DateTimeKind.Local).AddTicks(5872));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(1516));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(1921));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(1945));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(1962));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 667, DateTimeKind.Local).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 668, DateTimeKind.Local).AddTicks(496));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 668, DateTimeKind.Local).AddTicks(544));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 668, DateTimeKind.Local).AddTicks(1178));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 668, DateTimeKind.Local).AddTicks(1262));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(3837));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4174));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4199));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4216));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4232));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4252));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4269));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4285));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 675, DateTimeKind.Local).AddTicks(4301));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 671, DateTimeKind.Local).AddTicks(7523));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 671, DateTimeKind.Local).AddTicks(8139));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 671, DateTimeKind.Local).AddTicks(8168));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 671, DateTimeKind.Local).AddTicks(8190));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(582));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1271));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1304));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1334));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1353));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1375));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1395));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1413));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1433));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1453));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1472));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1490));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1510));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 672, DateTimeKind.Local).AddTicks(1530));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 667, DateTimeKind.Local).AddTicks(4530));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 7, 24, 5, 52, 50, 667, DateTimeKind.Local).AddTicks(7065));
        }
    }
}
