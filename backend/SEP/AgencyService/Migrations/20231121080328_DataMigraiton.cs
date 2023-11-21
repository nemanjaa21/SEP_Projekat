using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgencyService.Migrations
{
    public partial class DataMigraiton : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TotalPrice",
                table: "Offers",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "YearlyPrice",
                table: "OfferItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "MonthlyPrice",
                table: "OfferItems",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "OfferItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthlyPrice", "YearlyPrice" },
                values: new object[] { 24.989999999999998, 149.99000000000001 });

            migrationBuilder.InsertData(
                table: "OfferItems",
                columns: new[] { "Id", "IsAccepted", "MonthlyPrice", "OfferName", "YearlyPrice" },
                values: new object[,]
                {
                    { 2, false, 11.99, "ISSUANCE_OF_LAWS_ELECTRONIC_FORM", 99.989999999999995 },
                    { 3, false, 31.989999999999998, "ISSUANCE_OF_LAWS_PRINTED_FORM", 240.0 },
                    { 4, false, 6.9900000000000002, "PUBLICATION_OF_LAWS_ON_INTERNET", 39.990000000000002 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "TotalPrice",
                table: "Offers",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "YearlyPrice",
                table: "OfferItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "MonthlyPrice",
                table: "OfferItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "OfferItems",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MonthlyPrice", "YearlyPrice" },
                values: new object[] { 250, 1250 });
        }
    }
}
