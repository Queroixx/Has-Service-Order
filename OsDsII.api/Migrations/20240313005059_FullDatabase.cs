using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OsDsII.api.Migrations
{
    /// <inheritdoc />
    public partial class FullDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "service_order",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpeningDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 3, 12, 21, 50, 57, 907, DateTimeKind.Unspecified).AddTicks(8838), new TimeSpan(0, -3, 0, 0, 0))),
                    FinishDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_service_order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_service_order_customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comentario",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ServiceOrderId = table.Column<int>(type: "int", nullable: false),
                    SendDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 3, 12, 21, 50, 57, 908, DateTimeKind.Unspecified).AddTicks(4396), new TimeSpan(0, -3, 0, 0, 0)))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comentario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comentario_service_order_ServiceOrderId",
                        column: x => x.ServiceOrderId,
                        principalTable: "service_order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comentario_ServiceOrderId",
                table: "comentario",
                column: "ServiceOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_customer_Email",
                table: "customer",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_service_order_CustomerId",
                table: "service_order",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comentario");

            migrationBuilder.DropTable(
                name: "service_order");

            migrationBuilder.DropTable(
                name: "customer");
        }
    }
}
