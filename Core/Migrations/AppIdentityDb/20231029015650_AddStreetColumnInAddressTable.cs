using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations.AppIdentityDb
{
    public partial class AddStreetColumnInAddressTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Street",
                table: "Address");
        }
    }
}
