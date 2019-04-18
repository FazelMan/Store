using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Data.EntityFrameworkCore.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSku",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSku_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductSkuId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Purchase_ProductSku_ProductSkuId",
                        column: x => x.ProductSkuId,
                        principalTable: "ProductSku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedDate", "IsRemoved", "Title" },
                values: new object[] { 1, new DateTime(2019, 4, 18, 21, 8, 4, 795, DateTimeKind.Local).AddTicks(7267), false, "iPhone 7" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedDate", "IsRemoved", "Title" },
                values: new object[] { 2, new DateTime(2019, 4, 18, 21, 8, 4, 805, DateTimeKind.Local).AddTicks(8178), false, "Samsong A9" });

            migrationBuilder.InsertData(
                table: "ProductSku",
                columns: new[] { "Id", "CreatedDate", "IsRemoved", "Price", "ProductId", "Quantity", "Title", "UpdatedDate" },
                values: new object[] { 1, new DateTime(2019, 4, 18, 21, 8, 4, 810, DateTimeKind.Local).AddTicks(5222), false, 899m, 1, 10, "64G", null });

            migrationBuilder.InsertData(
                table: "ProductSku",
                columns: new[] { "Id", "CreatedDate", "IsRemoved", "Price", "ProductId", "Quantity", "Title", "UpdatedDate" },
                values: new object[] { 2, new DateTime(2019, 4, 18, 21, 8, 4, 811, DateTimeKind.Local).AddTicks(4508), false, 990m, 1, 5, "128G", null });

            migrationBuilder.InsertData(
                table: "ProductSku",
                columns: new[] { "Id", "CreatedDate", "IsRemoved", "Price", "ProductId", "Quantity", "Title", "UpdatedDate" },
                values: new object[] { 3, new DateTime(2019, 4, 18, 21, 8, 4, 811, DateTimeKind.Local).AddTicks(4703), false, 750m, 2, 20, "256G", null });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSku_ProductId",
                table: "ProductSku",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_ProductSkuId",
                table: "Purchase",
                column: "ProductSkuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "ProductSku");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
