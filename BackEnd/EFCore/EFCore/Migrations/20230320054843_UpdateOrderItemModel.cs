using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations;

public partial class UpdateOrderItemModel : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem");

        migrationBuilder.DropIndex(
            name: "IX_OrderItem_OrderId",
            table: "OrderItem");

        migrationBuilder.DropColumn(
            name: "id",
            table: "OrderItem");

        migrationBuilder.AlterColumn<decimal>(
            name: "UnitPrice",
            table: "OrderItem",
            type: "DECIMAL(18, 2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "DECIMAL(18,2)");

        migrationBuilder.AddPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem",
            columns: new[] { "OrderId", "ProductId" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem");

        migrationBuilder.AlterColumn<decimal>(
            name: "UnitPrice",
            table: "OrderItem",
            type: "DECIMAL(18,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "DECIMAL(18, 2)");

        migrationBuilder.AddColumn<int>(
            name: "id",
            table: "OrderItem",
            type: "NUMBER(10)",
            nullable: false,
            defaultValue: 0)
            .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1");

        migrationBuilder.AddPrimaryKey(
            name: "PK_OrderItem",
            table: "OrderItem",
            column: "id");

        migrationBuilder.CreateIndex(
            name: "IX_OrderItem_OrderId",
            table: "OrderItem",
            column: "OrderId");
    }
}
