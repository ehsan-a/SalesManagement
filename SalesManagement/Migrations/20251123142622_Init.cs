using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SalesManagement.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductType_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MinQuantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_ProductType_TypeId",
                        column: x => x.TypeId,
                        principalTable: "ProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransactionProduct_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Passive Components" },
                    { 2, "Semiconductors" },
                    { 3, "Voltage Regulators" },
                    { 4, "Microcontrollers" },
                    { 5, "Sensors" },
                    { 6, "Modules" },
                    { 7, "Connectors" },
                    { 8, "Power Electronics" },
                    { 9, "Displays" },
                    { 10, "Memory Chips" }
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Ali", "Khosravi" },
                    { 2, "Reza", "Ghavami" },
                    { 3, "Sina", "Ahmadi" },
                    { 4, "Nima", "Karimi" },
                    { 5, "Farhad", "Nikbakht" },
                    { 6, "Ehsan", "Rahmani" },
                    { 7, "Mina", "Sadeghi" },
                    { 8, "Mahdi", "Hashemi" },
                    { 9, "Parsa", "Ghanbari" },
                    { 10, "Hassan", "Moradi" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Ehsan", "Arefzadeh" },
                    { 2, "Reza", "Naghavi" },
                    { 3, "Sina", "Moradi" }
                });

            migrationBuilder.InsertData(
                table: "ProductType",
                columns: new[] { "Id", "CategoryId", "Title" },
                values: new object[,]
                {
                    { 1, 1, "1/4W Resistor" },
                    { 2, 1, "Electrolytic Capacitor" },
                    { 3, 2, "NPN Transistor" },
                    { 4, 2, "MOSFET" },
                    { 5, 3, "7805 Regulator" },
                    { 6, 4, "ATmega Series" },
                    { 7, 5, "Temperature Sensor" },
                    { 8, 6, "Relay Module" },
                    { 9, 7, "JST Connector" },
                    { 10, 9, "OLED Display" }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "CustomerId", "DateTime", "Type" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 11, 22, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5769), 0 },
                    { 2, 2, new DateTime(2025, 11, 21, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5786), 0 },
                    { 3, 3, new DateTime(2025, 11, 20, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5790), 0 },
                    { 4, 4, new DateTime(2025, 11, 19, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5792), 1 },
                    { 5, 5, new DateTime(2025, 11, 18, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5794), 1 },
                    { 6, 6, new DateTime(2025, 11, 17, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5796), 0 },
                    { 7, 7, new DateTime(2025, 11, 16, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5797), 1 },
                    { 8, 8, new DateTime(2025, 11, 15, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5798), 0 },
                    { 9, 9, new DateTime(2025, 11, 14, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5800), 1 },
                    { 10, 10, new DateTime(2025, 11, 13, 17, 56, 17, 730, DateTimeKind.Local).AddTicks(5801), 1 }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "IsActive", "MinQuantity", "Price", "Title", "TypeId" },
                values: new object[,]
                {
                    { 1, true, 10, 200m, "Resistor 10kΩ 1/4W", 1 },
                    { 2, true, 10, 150m, "Resistor 1kΩ 1/4W", 1 },
                    { 3, true, 20, 3000m, "Capacitor 100uF 25V", 2 },
                    { 4, true, 20, 3500m, "Capacitor 470uF 16V", 2 },
                    { 5, true, 10, 1200m, "Transistor 2N2222 NPN", 3 },
                    { 6, true, 5, 9000m, "MOSFET IRFZ44N", 4 },
                    { 7, true, 10, 4000m, "Voltage Regulator 7805", 5 },
                    { 8, true, 3, 58000m, "ATmega328P-PU", 6 },
                    { 9, true, 5, 25000m, "DS18B20 Temperature Sensor", 7 },
                    { 10, true, 2, 120000m, "OLED Display 0.96 I2C", 10 }
                });

            migrationBuilder.InsertData(
                table: "TransactionProduct",
                columns: new[] { "Id", "ProductId", "Quantity", "TransactionId", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 1000, 1, 200m },
                    { 2, 2, 800, 1, 150m },
                    { 3, 3, 200, 2, 3000m },
                    { 4, 4, 150, 2, 3500m },
                    { 5, 5, 100, 3, 1200m },
                    { 6, 6, 50, 3, 9000m },
                    { 7, 1, 100, 4, 200m },
                    { 8, 3, 20, 5, 3000m },
                    { 9, 8, 20, 6, 58000m },
                    { 10, 9, 40, 6, 25000m },
                    { 11, 9, 5, 7, 25000m },
                    { 12, 10, 10, 8, 120000m },
                    { 13, 10, 1, 9, 120000m },
                    { 14, 2, 50, 10, 150m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Product_TypeId",
                table: "Product",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductType_CategoryId",
                table: "ProductType",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProduct_ProductId",
                table: "TransactionProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionProduct_TransactionId",
                table: "TransactionProduct",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionProduct");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
