using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStore.Data.Migrations
{
    public partial class OrderChangeAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Town",
                table: "Orders",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Orders",
                newName: "Country");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "Town");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Orders",
                newName: "State");
        }
    }
}
