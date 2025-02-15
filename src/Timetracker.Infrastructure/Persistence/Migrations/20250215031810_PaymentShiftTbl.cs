using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acme.Timetracker.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PaymentShiftTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOnUtc",
                table: "Employees",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 3, 18, 10, 524, DateTimeKind.Unspecified).AddTicks(9677), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 2, 22, 55, 551, DateTimeKind.Unspecified).AddTicks(1802), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOnUtc",
                table: "Departments",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 3, 18, 10, 524, DateTimeKind.Unspecified).AddTicks(2362), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 2, 22, 55, 550, DateTimeKind.Unspecified).AddTicks(3720), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "PaymentShift",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    WeekDay = table.Column<int>(type: "int", nullable: true),
                    IsFullDay = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    StartAt = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndAt = table.Column<TimeOnly>(type: "time", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValue: new DateOnly(1, 1, 1)),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true, defaultValue: new DateOnly(9999, 12, 31)),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 3, 18, 10, 525, DateTimeKind.Unspecified).AddTicks(3036), new TimeSpan(0, 0, 0, 0, 0))),
                    UpdateOnUtc = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentShift", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentShift");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOnUtc",
                table: "Employees",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 2, 22, 55, 551, DateTimeKind.Unspecified).AddTicks(1802), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 3, 18, 10, 524, DateTimeKind.Unspecified).AddTicks(9677), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOnUtc",
                table: "Departments",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 2, 22, 55, 550, DateTimeKind.Unspecified).AddTicks(3720), new TimeSpan(0, 0, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2025, 2, 15, 3, 18, 10, 524, DateTimeKind.Unspecified).AddTicks(2362), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
