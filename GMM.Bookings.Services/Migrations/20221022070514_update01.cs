using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GMM.Bookings.Services.Migrations
{
  public partial class update01 : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Courses",
          columns: table => new
          {
            Id = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
            IsActive = table.Column<bool>(type: "bit", nullable: false),
            Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
            Hours = table.Column<double>(type: "float", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Courses", x => x.Id);
          });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Courses");
    }
  }
}
