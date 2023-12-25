using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgencyService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OfferItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferName = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "CODIFICATION_OF_LAWS"),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false),
                    MonthlyPrice = table.Column<int>(type: "int", nullable: false),
                    YearlyPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOfferServiceOfferItem",
                columns: table => new
                {
                    ServiceOfferItemsId = table.Column<int>(type: "int", nullable: false),
                    ServiceOffersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOfferServiceOfferItem", x => new { x.ServiceOfferItemsId, x.ServiceOffersId });
                    table.ForeignKey(
                        name: "FK_ServiceOfferServiceOfferItem_OfferItems_ServiceOfferItemsId",
                        column: x => x.ServiceOfferItemsId,
                        principalTable: "OfferItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOfferServiceOfferItem_Offers_ServiceOffersId",
                        column: x => x.ServiceOffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "OfferItems",
                columns: new[] { "Id", "IsAccepted", "MonthlyPrice", "YearlyPrice" },
                values: new object[] { 1, false, 250, 1250 });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOfferServiceOfferItem_ServiceOffersId",
                table: "ServiceOfferServiceOfferItem",
                column: "ServiceOffersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceOfferServiceOfferItem");

            migrationBuilder.DropTable(
                name: "OfferItems");

            migrationBuilder.DropTable(
                name: "Offers");
        }
    }
}
