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
                name: "StorageDepot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorageDepot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageDepotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentLevel = table.Column<int>(type: "int", nullable: false),
                    MaxLevel = table.Column<int>(type: "int", nullable: true),
                    MinLevel = table.Column<int>(type: "int", nullable: false),
                    AverageDemand = table.Column<int>(type: "int", nullable: true),
                    StandardDeviationDemand = table.Column<int>(type: "int", nullable: true),
                    Demand = table.Column<int>(type: "int", nullable: false),
                    LeadTime = table.Column<int>(type: "int", nullable: true),
                    OrderQuantity = table.Column<int>(type: "int", nullable: true),
                    OrderFrequency = table.Column<int>(type: "int", nullable: true),
                    HoldingCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderingCostPerOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ShortageCostPerUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    InventoryPosition = table.Column<int>(type: "int", nullable: true),
                    OrdersOutstanding = table.Column<int>(type: "int", nullable: true),
                    UnitsShort = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => new { x.ProductId, x.StorageDepotId });
                    table.ForeignKey(
                        name: "FK_Inventory_StorageDepot_StorageDepotId",
                        column: x => x.StorageDepotId,
                        principalTable: "StorageDepot",
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
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockReceipt_StorageDepot_StorageDepotId",
                        column: x => x.StorageDepotId,
                        principalTable: "StorageDepot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockReceipt_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockIssue",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StorageDepotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InventoryProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InventoryStorageDepotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockIssue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockIssue_Inventory_InventoryProductId_InventoryStorageDepotId",
                        columns: x => new { x.InventoryProductId, x.InventoryStorageDepotId },
                        principalTable: "Inventory",
                        principalColumns: new[] { "ProductId", "StorageDepotId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CurrentLevel",
                table: "Inventory",
                column: "CurrentLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MaxLevel",
                table: "Inventory",
                column: "MaxLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MinLevel",
                table: "Inventory",
                column: "MinLevel");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ProductId",
                table: "Inventory",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_StorageDepotId",
                table: "Inventory",
                column: "StorageDepotId");

            migrationBuilder.CreateIndex(
                name: "IX_StockIssue_InventoryProductId_InventoryStorageDepotId",
                table: "StockIssue",
                columns: new[] { "InventoryProductId", "InventoryStorageDepotId" });

            migrationBuilder.CreateIndex(
                name: "IX_StockIssue_IssueDate",
                table: "StockIssue",
                column: "IssueDate");

            migrationBuilder.CreateIndex(
                name: "IX_StockIssue_ProductId",
                table: "StockIssue",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockIssue_StorageDepotId",
                table: "StockIssue",
                column: "StorageDepotId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_ProductId",
                table: "StockReceipt",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_ReceiptDate",
                table: "StockReceipt",
                column: "ReceiptDate");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_StorageDepotId",
                table: "StockReceipt",
                column: "StorageDepotId");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceipt_SupplierId",
                table: "StockReceipt",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_StorageDepot_Address",
                table: "StorageDepot",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_StorageDepot_Name",
                table: "StorageDepot",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Address",
                table: "Supplier",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Email",
                table: "Supplier",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Name",
                table: "Supplier",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Phone",
                table: "Supplier",
                column: "Phone");
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
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "StorageDepot");
        }
    }
}
