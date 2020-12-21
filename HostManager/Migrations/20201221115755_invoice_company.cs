using Microsoft.EntityFrameworkCore.Migrations;

namespace HostManager.Migrations
{
    public partial class invoice_company : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Invoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Invoices");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Invoices",
                type: "text",
                nullable: true);
        }
    }
}
