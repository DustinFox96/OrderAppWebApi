using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderAppWebApi.Migrations
{
    public partial class addedSalespersonandcontroller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Salespersonid",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "salespeople",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    StateCode = table.Column<string>(maxLength: 2, nullable: true),
                    Sales = table.Column<decimal>(type: "decimal (9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salespeople", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Salespersonid",
                table: "Orders",
                column: "Salespersonid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_salespeople_Salespersonid",
                table: "Orders",
                column: "Salespersonid",
                principalTable: "salespeople",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_salespeople_Salespersonid",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "salespeople");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Salespersonid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Salespersonid",
                table: "Orders");
        }
    }
}
