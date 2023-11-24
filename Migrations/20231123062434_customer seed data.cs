using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestBankingAPI.Migrations
{
    public partial class customerseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "Name" },
                values: new object[,]
                {
                    { 1, "Arisha Barron" },
                    { 2, "Branden Gibson" },
                    { 3, "Rhonda Church" },
                    { 4, "Georgina Hazel" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 4);
        }
    }
}
