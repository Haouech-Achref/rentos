using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Rentos.Migrations
{
    public partial class rent_int_to_string : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {



            migrationBuilder.AlterColumn<string>(
                name: "User_UserId",
                table: "Rent",
                nullable: false,
                oldClrType: typeof(int));

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
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_Car_Car_CarId",
                table: "Rent");

            migrationBuilder.DropForeignKey(
                name: "FK_Rent_ApplicationUser_User_UserId",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_User_UserId",
                table: "Rent");

            migrationBuilder.AlterColumn<int>(
                name: "User_UserId",
                table: "Rent",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "Rent",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Rent",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rent_CarId",
                table: "Rent",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_UserId",
                table: "Rent",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_Car_CarId",
                table: "Rent",
                column: "CarId",
                principalTable: "Car",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_ApplicationUser_UserId",
                table: "Rent",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
