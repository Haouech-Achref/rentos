using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Rentos.Migrations
{
    public partial class rentchange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {



            migrationBuilder.DropIndex(
                name: "IX_Rent_Car_CarId",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_User_UserId",
                table: "Rent");

            migrationBuilder.AlterColumn<string>(
                name: "User_UserId",
                table: "Rent",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Rent",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rent_CarId",
                table: "Rent",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Car_CarId",
                table: "Rent",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Car_CarId",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_CarId",
                table: "Rent");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "Rent");

            migrationBuilder.AlterColumn<string>(
                name: "User_UserId",
                table: "Rent",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rent_Car_CarId",
                table: "Rent",
                column: "Car_CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_User_UserId",
                table: "Rent",
                column: "User_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Car_Car_CarId",
                table: "Rent",
                column: "Car_CarId",
                principalTable: "Car",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_ApplicationUser_User_UserId",
                table: "Rent",
                column: "User_UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
