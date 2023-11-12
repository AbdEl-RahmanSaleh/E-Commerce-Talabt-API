using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Core.Migrations
{
    public partial class AddPaymentIntentIdColumnToOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "shippingAddress_ZipCode",
                table: "Orders",
                newName: "ShippingAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "shippingAddress_Street",
                table: "Orders",
                newName: "ShippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "shippingAddress_State",
                table: "Orders",
                newName: "ShippingAddress_State");

            migrationBuilder.RenameColumn(
                name: "shippingAddress_LastName",
                table: "Orders",
                newName: "ShippingAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "shippingAddress_FirstName",
                table: "Orders",
                newName: "ShippingAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "shippingAddress_City",
                table: "Orders",
                newName: "ShippingAddress_City");

            migrationBuilder.AddColumn<string>(
                name: "PaymentIntentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentIntentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_ZipCode",
                table: "Orders",
                newName: "shippingAddress_ZipCode");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Street",
                table: "Orders",
                newName: "shippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_State",
                table: "Orders",
                newName: "shippingAddress_State");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_LastName",
                table: "Orders",
                newName: "shippingAddress_LastName");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_FirstName",
                table: "Orders",
                newName: "shippingAddress_FirstName");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_City",
                table: "Orders",
                newName: "shippingAddress_City");
        }
    }
}
