using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgencyService.Migrations
{
    public partial class SelectedPriceMigraiton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "SelectedPrice",
                table: "OfferItems",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedPrice",
                table: "OfferItems");
        }
    }
}
