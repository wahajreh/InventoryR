using Microsoft.EntityFrameworkCore.Migrations;

namespace mysite.Migrations
{
    public partial class AddWorkOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WorkOrder",
                table: "Customers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkOrder",
                table: "Customers");
        }
    }
}
