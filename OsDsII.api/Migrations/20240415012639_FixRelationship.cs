using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsDsII.api.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentario_service_order_ServiceOrderId",
                table: "comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_customer_CustomerId",
                table: "service_order");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OpeningDate",
                table: "service_order",
                type: "datetimeoffset",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 3, 12, 21, 50, 57, 907, DateTimeKind.Unspecified).AddTicks(8838), new TimeSpan(0, -3, 0, 0, 0)));

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SendDate",
                table: "comentario",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 4, 14, 22, 26, 37, 932, DateTimeKind.Unspecified).AddTicks(616), new TimeSpan(0, -3, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 3, 12, 21, 50, 57, 908, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, -3, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_comentario_service_order_ServiceOrderId",
                table: "comentario",
                column: "ServiceOrderId",
                principalTable: "service_order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_customer_CustomerId",
                table: "service_order",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_comentario_service_order_ServiceOrderId",
                table: "comentario");

            migrationBuilder.DropForeignKey(
                name: "FK_service_order_customer_CustomerId",
                table: "service_order");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "OpeningDate",
                table: "service_order",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 3, 12, 21, 50, 57, 907, DateTimeKind.Unspecified).AddTicks(8838), new TimeSpan(0, -3, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "SendDate",
                table: "comentario",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(2024, 3, 12, 21, 50, 57, 908, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, -3, 0, 0, 0)),
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldDefaultValue: new DateTimeOffset(new DateTime(2024, 4, 14, 22, 26, 37, 932, DateTimeKind.Unspecified).AddTicks(616), new TimeSpan(0, -3, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_comentario_service_order_ServiceOrderId",
                table: "comentario",
                column: "ServiceOrderId",
                principalTable: "service_order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_service_order_customer_CustomerId",
                table: "service_order",
                column: "CustomerId",
                principalTable: "customer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
