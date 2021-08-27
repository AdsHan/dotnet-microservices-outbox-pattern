using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace MOP.Order.Infrastructure.Migrations
{
    public partial class Criacao_Projeto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Phone = table.Column<string>(type: "CHAR(11)", nullable: true),
                    Email = table.Column<string>(type: "CHAR(254)", nullable: false),
                    CEP = table.Column<string>(type: "CHAR(8)", nullable: false),
                    State = table.Column<string>(type: "CHAR(2)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BarCode = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(60)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedIn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    FinishedIn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    Shipping = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observation = table.Column<string>(type: "VARCHAR(512)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    DiscountValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DateCreateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleteAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_customer_phone",
                table: "Customers",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.Sql("insert Customers values('A9E5B222-313C-4AE2-8E04-809C3CFF4A80', 'Cliente Demonstração', '51999123456', 'test@gamil.com','99900000', 'RS', 'Porto Alegre', 1, CURRENT_TIMESTAMP, null)");
            migrationBuilder.Sql("insert Products values('BCE4F473-3DFA-4FB9-8E1E-5997951F5485', '78911122233344', 'Notebook MOP', 4500.21, 0, 1, CURRENT_TIMESTAMP, null);");
            migrationBuilder.Sql("insert Products values('92F31F2B-B0ED-4A5A-4C38-08D964E98AEC', '78911122299988', 'Monitor LED 19,5” HQ-LED HDMI', 575.91, 0, 1, CURRENT_TIMESTAMP, null);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
