using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace talabat.Repository.Data.Migrations
{
    public partial class OrderEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deliveryMethods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_deliveryMethods", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShipingAddress_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeliveryMethodid = table.Column<int>(type: "int", nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayemnyIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_deliveryMethods_DeliveryMethodid",
                        column: x => x.DeliveryMethodid,
                        principalTable: "deliveryMethods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ProductId = table.Column<int>(type: "int", nullable: false),
                    Product_ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Product_PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Orderid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.id);
                    table.ForeignKey(
                        name: "FK_orderItems_orders_Orderid",
                        column: x => x.Orderid,
                        principalTable: "orders",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_Orderid",
                table: "orderItems",
                column: "Orderid");

            migrationBuilder.CreateIndex(
                name: "IX_orders_DeliveryMethodid",
                table: "orders",
                column: "DeliveryMethodid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "deliveryMethods");
        }
    }
}
