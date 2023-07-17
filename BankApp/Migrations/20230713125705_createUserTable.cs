using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BankApp.Migrations
{
    /// <inheritdoc />
    public partial class createUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VerifyEmail = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedAt", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "VerifyEmail" },
                values: new object[,]
                {
                    { 1, "sfsff", new DateTime(2023, 7, 13, 14, 57, 5, 520, DateTimeKind.Local).AddTicks(9684), "Test", "Test", "Test", "Test", "Test", false },
                    { 2, "sfsff", new DateTime(2023, 7, 13, 14, 57, 5, 520, DateTimeKind.Local).AddTicks(9716), "Test", "Test2", "Test2", "Test", "Test", false },
                    { 3, "sfsff", new DateTime(2023, 7, 13, 14, 57, 5, 520, DateTimeKind.Local).AddTicks(9719), "Test", "Test3", "Test3", "Test", "Test", false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
