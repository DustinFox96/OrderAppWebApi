using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderAppWebApi.Migrations
{
    public partial class madesalespersonFKnotnullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_salespeople_Salespersonid",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Salespersonid",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_salespeople_Salespersonid",
                table: "Orders",
                column: "Salespersonid",
                principalTable: "salespeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_salespeople_Salespersonid",
                table: "Orders");

            migrationBuilder.AlterColumn<int>(
                name: "Salespersonid",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_salespeople_Salespersonid",
                table: "Orders",
                column: "Salespersonid",
                principalTable: "salespeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
