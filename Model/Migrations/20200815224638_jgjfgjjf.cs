using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Model.Migrations
{
    public partial class jgjfgjjf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Technicals_TechnicalId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_TechnicalId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsOrder",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TechnicalId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ResponsibleRate",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResponsibleReview",
                table: "Orders",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 476, DateTimeKind.Local).AddTicks(7071));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 489, DateTimeKind.Local).AddTicks(6416));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 489, DateTimeKind.Local).AddTicks(7237));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 489, DateTimeKind.Local).AddTicks(7271));

            migrationBuilder.UpdateData(
                table: "JobTitles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 489, DateTimeKind.Local).AddTicks(7291));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 479, DateTimeKind.Local).AddTicks(4271));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 479, DateTimeKind.Local).AddTicks(5352));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 479, DateTimeKind.Local).AddTicks(5385));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 479, DateTimeKind.Local).AddTicks(6152));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 479, DateTimeKind.Local).AddTicks(6254));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(2600));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3345));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3377));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3398));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3446));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3465));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3484));

            migrationBuilder.UpdateData(
                table: "Ordertrackaction",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 490, DateTimeKind.Local).AddTicks(3503));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 486, DateTimeKind.Local).AddTicks(2480));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 486, DateTimeKind.Local).AddTicks(3430));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 486, DateTimeKind.Local).AddTicks(3463));

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 486, DateTimeKind.Local).AddTicks(3487));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 486, DateTimeKind.Local).AddTicks(8905));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(260));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(300));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(325));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 5L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(380));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(414));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 7L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(457));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 8L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(480));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 9L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(501));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 10L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(526));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 11L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(547));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 12L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(569));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 13L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(589));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 14L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(609));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 15L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(629));

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 487, DateTimeKind.Local).AddTicks(650));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 478, DateTimeKind.Local).AddTicks(3349));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 479, DateTimeKind.Local).AddTicks(426));

            migrationBuilder.UpdateData(
                table: "SystemOptions",
                keyColumn: "Id",
                keyValue: 3L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 479, DateTimeKind.Local).AddTicks(588));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 477, DateTimeKind.Local).AddTicks(9698));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 16, 0, 46, 37, 478, DateTimeKind.Local).AddTicks(1698));

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserName",
                table: "Customer",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customer_UserName",
                table: "Customer");

            migrationBuilder.DropColumn(
                name: "ResponsibleRate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ResponsibleReview",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrder",
                table: "Orders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "TechnicalId",
                table: "Orders",
                nullable: true);

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
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(4960));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 4L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(6082));

            migrationBuilder.UpdateData(
                table: "Modules",
                keyColumn: "Id",
                keyValue: 6L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 220, DateTimeKind.Local).AddTicks(6213));

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

            migrationBuilder.UpdateData(
                table: "Screens",
                keyColumn: "Id",
                keyValue: 16L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 226, DateTimeKind.Local).AddTicks(1737));

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
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 219, DateTimeKind.Local).AddTicks(2244));

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
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 217, DateTimeKind.Local).AddTicks(7937));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                column: "CreationDate",
                value: new DateTime(2020, 8, 15, 10, 6, 7, 217, DateTimeKind.Local).AddTicks(9792));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TechnicalId",
                table: "Orders",
                column: "TechnicalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Technicals_TechnicalId",
                table: "Orders",
                column: "TechnicalId",
                principalTable: "Technicals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
