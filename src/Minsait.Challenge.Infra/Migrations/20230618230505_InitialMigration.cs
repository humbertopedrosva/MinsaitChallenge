using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Minsait.Challenge.Infra.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Merchant",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchant", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Merchant",
                columns: new[] { "Id", "Email", "Name", "PasswordHash", "Surname" },
                values: new object[] { new Guid("661b8028-6ce0-4544-950d-18837c2bcd7e"), "admin@admin.com", "Admin", "18a948b42a6f1fa8b84bfc73c8a967b1df15ee4dbd08e9bd150441b5e576698c", "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchant");
        }
    }
}
