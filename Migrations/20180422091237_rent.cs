using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Rentos.Migrations
{
    public partial class rent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(name: "Rent"
                , columns: table => new
                {

                    Car_CarId = table.Column<int>(nullable: false),
                    User_UserId = table.Column<string>(nullable: false),
                    PickupDate = table.Column<DateTime>(nullable: false),
                    DropoffDate = table.Column<DateTime>(nullable: false)
                }, constraints: table =>
                {
                    table.PrimaryKey("Pk_Rent", x => new { x.Car_CarId, x.User_UserId});
                }

                );


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Rent",
                table: "Rent");

            migrationBuilder.DropColumn(
                name: "DropoffDate",
                table: "Rent");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "Rent");

            migrationBuilder.AlterColumn<int>(
                name: "Car_CarId",
                table: "Rent",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rent",
                table: "Rent",
                column: "Car_CarId");
        }
    }
}
