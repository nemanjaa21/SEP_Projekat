using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgencyService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OfferItems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OfferItems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OfferItems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "OfferItems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.AddColumn<int>(
                name: "AgencyId",
                table: "OfferItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SelectedPrice",
                table: "OfferItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Agencies_AgencyId",
                        column: x => x.AgencyId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyPaymentService",
                columns: table => new
                {
                    AgenciesId = table.Column<int>(type: "int", nullable: false),
                    PaymentServicesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyPaymentService", x => new { x.AgenciesId, x.PaymentServicesId });
                    table.ForeignKey(
                        name: "FK_AgencyPaymentService_Agencies_AgenciesId",
                        column: x => x.AgenciesId,
                        principalTable: "Agencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyPaymentService_PaymentServices_PaymentServicesId",
                        column: x => x.PaymentServicesId,
                        principalTable: "PaymentServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Agencies",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Agency" });

            migrationBuilder.CreateIndex(
                name: "IX_OfferItems_AgencyId",
                table: "OfferItems",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyPaymentService_PaymentServicesId",
                table: "AgencyPaymentService",
                column: "PaymentServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AgencyId",
                table: "Users",
                column: "AgencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferItems_Agencies_AgencyId",
                table: "OfferItems",
                column: "AgencyId",
                principalTable: "Agencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OfferItems_Agencies_AgencyId",
                table: "OfferItems");

            migrationBuilder.DropTable(
                name: "AgencyPaymentService");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "PaymentServices");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropIndex(
                name: "IX_OfferItems_AgencyId",
                table: "OfferItems");

            migrationBuilder.DropColumn(
                name: "AgencyId",
                table: "OfferItems");

            migrationBuilder.DropColumn(
                name: "SelectedPrice",
                table: "OfferItems");

            migrationBuilder.InsertData(
                table: "OfferItems",
                columns: new[] { "Id", "IsAccepted", "MonthlyPrice", "OfferName", "YearlyPrice" },
                values: new object[,]
                {
                    { 1, false, 24.989999999999998, "CODIFICATION_OF_LAWS", 149.99000000000001 },
                    { 2, false, 11.99, "ISSUANCE_OF_LAWS_ELECTRONIC_FORM", 99.989999999999995 },
                    { 3, false, 31.989999999999998, "ISSUANCE_OF_LAWS_PRINTED_FORM", 240.0 },
                    { 4, false, 6.9900000000000002, "PUBLICATION_OF_LAWS_ON_INTERNET", 39.990000000000002 }
                });
        }
    }
}
