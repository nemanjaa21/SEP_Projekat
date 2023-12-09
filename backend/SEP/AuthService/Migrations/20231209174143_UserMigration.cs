using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthService.Migrations
{
    public partial class UserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password", "Type" },
                values: new object[] { 1, "zdravko@gmail.com", "$2a$11$LJD2FpYMIxY8G/WrKvUobeR0yLVktJzaB93JaO3NA/5vxN36BefHC", 0 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password", "Type" },
                values: new object[] { 2, "kurda@gmail.com", "$2a$11$.iaAO0HD4UUKWo5dYKzCgeGSG4pCrnhBdhzC7caQVdSxS7eGIvVnm", 2 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Email", "Password", "Type" },
                values: new object[] { 3, "malina@gmail.com", "$2a$11$DngOWHvIh42rjiVEw8M5v.94Z9gWm76FgKEghb1bxkIGerRufE3L.", 6 });

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
