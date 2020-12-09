using Microsoft.EntityFrameworkCore.Migrations;

namespace PerpusBelajar.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ISBN = table.Column<string>(maxLength: 13, nullable: false),
                    Title = table.Column<string>(maxLength: 100, nullable: false),
                    Author = table.Column<string>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ImageFileName = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "Id", "Author", "Category", "ISBN", "ImageFileName", "Quantity", "Title" },
                values: new object[] { 1, "JK Rowling", 5, "000000000001", "Book1.jpg", 1, "Harry Potter and The Sorcerer's Stone" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
