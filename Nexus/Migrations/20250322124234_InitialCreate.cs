using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PONumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Partner = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceContact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SalesContact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    WarrantyStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarrantyEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BreakDownOpen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProblemReported = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AnyPatientHarm = table.Column<bool>(type: "bit", nullable: false),
                    PatientHarmDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    MachineStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakDownOpen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakDownOpen_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calibration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ServiceReportPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NextCalibrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calibration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calibration_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    ContractNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContractPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContractOrders_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstallationBases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Importance = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PMFrequency = table.Column<int>(type: "int", nullable: false),
                    PMCount = table.Column<int>(type: "int", nullable: false),
                    CMFrequency = table.Column<int>(type: "int", nullable: false),
                    CMCount = table.Column<int>(type: "int", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallationBases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallationBases_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    NextPMDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PM_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BreakDownClose",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreakDownOpenId = table.Column<int>(type: "int", nullable: false),
                    ActionTaken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PartRequired = table.Column<bool>(type: "bit", nullable: false),
                    FollowUpAction = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakDownClose", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BreakDownClose_BreakDownOpen_BreakDownOpenId",
                        column: x => x.BreakDownOpenId,
                        principalTable: "BreakDownOpen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BreakDownCloseEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PartDetail_BreakDownClose_BreakDownCloseEntityId",
                        column: x => x.BreakDownCloseEntityId,
                        principalTable: "BreakDownClose",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BreakDownClose_BreakDownOpenId",
                table: "BreakDownClose",
                column: "BreakDownOpenId");

            migrationBuilder.CreateIndex(
                name: "IX_BreakDownOpen_PurchaseOrderId",
                table: "BreakDownOpen",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Calibration_PurchaseOrderId",
                table: "Calibration",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractOrders_PurchaseOrderId",
                table: "ContractOrders",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallationBases_PurchaseOrderId",
                table: "InstallationBases",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_PartDetail_BreakDownCloseEntityId",
                table: "PartDetail",
                column: "BreakDownCloseEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PM_PurchaseOrderId",
                table: "PM",
                column: "PurchaseOrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calibration");

            migrationBuilder.DropTable(
                name: "ContractOrders");

            migrationBuilder.DropTable(
                name: "InstallationBases");

            migrationBuilder.DropTable(
                name: "PartDetail");

            migrationBuilder.DropTable(
                name: "PM");

            migrationBuilder.DropTable(
                name: "BreakDownClose");

            migrationBuilder.DropTable(
                name: "BreakDownOpen");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");
        }
    }
}
