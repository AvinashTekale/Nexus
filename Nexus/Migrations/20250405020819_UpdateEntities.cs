using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nexus.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "SalesContact",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "SerialNumber",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ServiceContact",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "WarrantyEndDate",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "WarrantyStartDate",
                table: "PurchaseOrders");

            migrationBuilder.CreateTable(
                name: "PurchaseChildren",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServiceContactId = table.Column<int>(type: "int", nullable: false),
                    SalesContactId = table.Column<int>(type: "int", nullable: false),
                    WarrantyStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WarrantyEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseChildren", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseChildren_ContactPersons_SalesContactId",
                        column: x => x.SalesContactId,
                        principalTable: "ContactPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseChildren_ContactPersons_ServiceContactId",
                        column: x => x.ServiceContactId,
                        principalTable: "ContactPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseChildren_EquipmentCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "EquipmentCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseChildren_EquipmentModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "EquipmentModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseChildren_PurchaseOrders_PurchaseId",
                        column: x => x.PurchaseId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseChildren_CategoryId",
                table: "PurchaseChildren",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseChildren_ModelId",
                table: "PurchaseChildren",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseChildren_PurchaseId",
                table: "PurchaseChildren",
                column: "PurchaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseChildren_SalesContactId",
                table: "PurchaseChildren",
                column: "SalesContactId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseChildren_ServiceContactId",
                table: "PurchaseChildren",
                column: "ServiceContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseChildren");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "PurchaseOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "PurchaseOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PurchaseOrders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "SalesContact",
                table: "PurchaseOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SerialNumber",
                table: "PurchaseOrders",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServiceContact",
                table: "PurchaseOrders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyEndDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyStartDate",
                table: "PurchaseOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
