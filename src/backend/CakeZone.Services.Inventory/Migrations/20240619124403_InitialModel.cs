using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CakeZone.Services.Inventory.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageDepotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentLevel = table.Column<int>(type: "int", nullable: false),
                    MaxLevel = table.Column<int>(type: "int", nullable: false),
                    MinLevel = table.Column<int>(type: "int", nullable: false),
                    AverageDemand = table.Column<int>(type: "int", nullable: false),
                    StandardDeviationDemand = table.Column<int>(type: "int", nullable: false),
                    Demand = table.Column<int>(type: "int", nullable: false),
                    LeadTime = table.Column<int>(type: "int", nullable: false),
                    OrderQuantity = table.Column<int>(type: "int", nullable: true),
                    OrderFrequency = table.Column<int>(type: "int", nullable: true),
                    HoldingCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderingCostPerOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShortageCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InventoryPosition = table.Column<int>(type: "int", nullable: false),
                    OrdersOutstanding = table.Column<int>(type: "int", nullable: true),
                    UnitsShort = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => new { x.ProductId, x.StorageDepotId });
                });

            migrationBuilder.CreateTable(
                name: "Storage_Depot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage_Depot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StockIssue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Storage_DepotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockIssue_Inventory_ProductId_Storage_DepotId",
                        columns: x => new { x.ProductId, x.Storage_DepotId },
                        principalTable: "Inventory",
                        principalColumns: new[] { "ProductId", "StorageDepotId" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockIssue_Storage_Depot_Storage_DepotId",
                        column: x => x.Storage_DepotId,
                        principalTable: "Storage_Depot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockReceipt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SupplierId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageDepotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Storage_DepotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceipt_Storage_Depot_Storage_DepotId",
                        column: x => x.Storage_DepotId,
                        principalTable: "Storage_Depot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceipt_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockIssue_ProductId_Storage_DepotId",
                table: "StockIssue",
                columns: new[] { "ProductId", "Storage_DepotId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockIssue_Storage_DepotId",
                table: "StockIssue",
                column: "Storage_DepotId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_Storage_DepotId",
                table: "StockReceipt",
                column: "Storage_DepotId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_SupplierId",
                table: "StockReceipt",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockIssue");

            migrationBuilder.DropTable(
                name: "StockReceipt");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Storage_Depot");

            migrationBuilder.DropTable(
                name: "Supplier");
        }
    }
}
