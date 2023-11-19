using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentServiceProvider.Migrations
{
    public partial class PaymentServicesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentServices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PaymentServices",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Credit Card Payment" },
                    { 2, "Bitcoin Payment" },
                    { 3, "QR Code Payment" },
                    { 4, "PayPal Payment" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentServices");
        }
    }
}
