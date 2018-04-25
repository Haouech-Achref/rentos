using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Rentos.Migrations
{
    public partial class rentpk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropPrimaryKey(
                name: "PK_Rent",
                table: "Rent");

            migrationBuilder.AlterColumn<string>(
                name: "User_UserId",
                table: "Rent",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "RentId",
                table: "Rent",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rent",
                table: "Rent",
                column: "RentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rent_Car_CarId",
                table: "Rent",
                column: "Car_CarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rent_ApplicationUser_User_UserId",
                table: "Rent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rent",
                table: "Rent");

            migrationBuilder.DropIndex(
                name: "IX_Rent_Car_CarId",
                table: "Rent");

            migrationBuilder.DropColumn(
                name: "RentId",
                table: "Rent");

            migrationBuilder.AlterColumn<string>(
                name: "User_UserId",
                table: "Rent",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rent",
                table: "Rent",
                columns: new[] { "Car_CarId", "User_UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Rent_ApplicationUser_User_UserId",
                table: "Rent",
                column: "User_UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
