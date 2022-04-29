using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUSL2020.Infrastructure.Data.Migrations
{
    public partial class InstituteEntityRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "RdaOffices");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "RdaOffices");

            migrationBuilder.DropColumn(
                name: "Address_Line1",
                table: "RdaOffices");

            migrationBuilder.DropColumn(
                name: "Address_Line2",
                table: "RdaOffices");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "RdaOffices");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "RdaOffices");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Address_Line1",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Address_Line2",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Address_City",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Address_District",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Address_Line1",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Address_Line2",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Address_Street",
                table: "Insurances");

            migrationBuilder.DropColumn(
                name: "Address_ZipCode",
                table: "Insurances");

            migrationBuilder.AlterColumn<int>(
                name: "Owner_Address_District",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Address_District",
                table: "Reporters",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Division",
                table: "PoliceStations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "PoliceStations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Division",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "PoliceStations");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Insurances");

            migrationBuilder.AlterColumn<string>(
                name: "Owner_Address_District",
                table: "Vehicles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Address_District",
                table: "Reporters",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "RdaOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "RdaOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Line1",
                table: "RdaOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Line2",
                table: "RdaOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "RdaOffices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Address_ZipCode",
                table: "RdaOffices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "PoliceStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "PoliceStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Line1",
                table: "PoliceStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Line2",
                table: "PoliceStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "PoliceStations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Address_ZipCode",
                table: "PoliceStations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_City",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_District",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Line1",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Line2",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address_Street",
                table: "Insurances",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Address_ZipCode",
                table: "Insurances",
                type: "int",
                nullable: true);
        }
    }
}
