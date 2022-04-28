using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PUSL2020.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ZipCode = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliceStations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ZipCode = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliceStations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RdaOffices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    District = table.Column<int>(type: "int", nullable: false),
                    Address_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_ZipCode = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RdaOffices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReporterUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReporterUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebMasters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebMasters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RdaOfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_EmployeeUsers_Id",
                        column: x => x.Id,
                        principalTable: "EmployeeUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_RdaOffices_RdaOfficeId",
                        column: x => x.RdaOfficeId,
                        principalTable: "RdaOffices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reporters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReporterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_ZipCode = table.Column<int>(type: "int", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Crn = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nic = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverLicenseId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reporters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reporters_ReporterUser_Id",
                        column: x => x.Id,
                        principalTable: "ReporterUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Insurance_Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HeadOfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Insurance_Employees_Employees_Id",
                        column: x => x.Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Insurance_Employees_Insurances_HeadOfficeId",
                        column: x => x.HeadOfficeId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Police_Officers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Police_Officers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Police_Officers_Employees_Id",
                        column: x => x.Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Police_Officers_PoliceStations_StationId",
                        column: x => x.StationId,
                        principalTable: "PoliceStations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rda_Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OfficeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rda_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rda_Employees_Employees_Id",
                        column: x => x.Id,
                        principalTable: "Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rda_Employees_RdaOffices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "RdaOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vrn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    EngineNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Owner_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Address_Line1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Address_Line2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner_Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner_Address_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Owner_Address_District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Address_ZipCode = table.Column<int>(type: "int", nullable: true),
                    Owner_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Insurance_PolicyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Insurance_StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Insurance_ExpiryAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Insurance_IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.UniqueConstraint("AK_Vehicles_ReporterId_Vrn", x => new { x.ReporterId, x.Vrn });
                    table.ForeignKey(
                        name: "FK_Vehicles_Insurances_Insurance_IssuerId",
                        column: x => x.Insurance_IssuerId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicles_Reporters_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Reporters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Accidents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReporterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Driver_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Driver_Dln = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location_Road = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location_District = table.Column<int>(type: "int", nullable: false),
                    Location_Latitude = table.Column<decimal>(type: "decimal(10,8)", precision: 10, scale: 8, nullable: true),
                    Location_Longitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: true),
                    Cause = table.Column<int>(type: "int", nullable: false),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Reported = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Archived = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RdaApproval_IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    RdaApproval_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RdaApproval_EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PoliceApproval_IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    PoliceApproval_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PoliceApproval_EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    InsuranceApproval_IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    InsuranceApproval_Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceApproval_EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accidents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accidents_Insurance_Employees_InsuranceApproval_EmployeeId",
                        column: x => x.InsuranceApproval_EmployeeId,
                        principalTable: "Insurance_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accidents_Police_Officers_PoliceApproval_EmployeeId",
                        column: x => x.PoliceApproval_EmployeeId,
                        principalTable: "Police_Officers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accidents_Rda_Employees_RdaApproval_EmployeeId",
                        column: x => x.RdaApproval_EmployeeId,
                        principalTable: "Rda_Employees",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Accidents_Reporters_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Reporters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Accidents_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accidents",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Vehicle_Snapshots",
                columns: table => new
                {
                    AccidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Vrn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuelType = table.Column<int>(type: "int", nullable: false),
                    EngineNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegisteredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Owner_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Owner_Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Insurance_PolicyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Insurance_StartAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Insurance_ExpiryAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Insurance_IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle_Snapshots", x => x.AccidentId);
                    table.ForeignKey(
                        name: "FK_Vehicle_Snapshots_Accidents_AccidentId",
                        column: x => x.AccidentId,
                        principalTable: "Accidents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Vehicle_Snapshots_Insurances_Insurance_IssuerId",
                        column: x => x.Insurance_IssuerId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_InsuranceApproval_EmployeeId",
                table: "Accidents",
                column: "InsuranceApproval_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_PoliceApproval_EmployeeId",
                table: "Accidents",
                column: "PoliceApproval_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_RdaApproval_EmployeeId",
                table: "Accidents",
                column: "RdaApproval_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Accidents_ReporterId",
                table: "Accidents",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RdaOfficeId",
                table: "Employees",
                column: "RdaOfficeId");

            migrationBuilder.CreateIndex(
                name: "Employee_UsernameIndex",
                table: "EmployeeUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AccidentId",
                table: "Images",
                column: "AccidentId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_Employees_HeadOfficeId",
                table: "Insurance_Employees",
                column: "HeadOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Police_Officers_StationId",
                table: "Police_Officers",
                column: "StationId");

            migrationBuilder.CreateIndex(
                name: "IX_Rda_Employees_OfficeId",
                table: "Rda_Employees",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reporters_Crn",
                table: "Reporters",
                column: "Crn",
                unique: true,
                filter: "[Crn] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reporters_DriverLicenseId",
                table: "Reporters",
                column: "DriverLicenseId",
                unique: true,
                filter: "[DriverLicenseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reporters_Nic",
                table: "Reporters",
                column: "Nic",
                unique: true,
                filter: "[Nic] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Reporter_EmailIndex",
                table: "ReporterUser",
                column: "NormalizedEmail",
                unique: true,
                filter: "[NormalizedEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_Snapshots_Insurance_IssuerId",
                table: "Vehicle_Snapshots",
                column: "Insurance_IssuerId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Insurance_IssuerId",
                table: "Vehicles",
                column: "Insurance_IssuerId");

            migrationBuilder.CreateIndex(
                name: "WebMaster_UsernameIndex",
                table: "WebMasters",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Vehicle_Snapshots");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "WebMasters");

            migrationBuilder.DropTable(
                name: "Accidents");

            migrationBuilder.DropTable(
                name: "Insurance_Employees");

            migrationBuilder.DropTable(
                name: "Police_Officers");

            migrationBuilder.DropTable(
                name: "Rda_Employees");

            migrationBuilder.DropTable(
                name: "Reporters");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "PoliceStations");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ReporterUser");

            migrationBuilder.DropTable(
                name: "EmployeeUsers");

            migrationBuilder.DropTable(
                name: "RdaOffices");
        }
    }
}
