using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudeDemmo.Migrations
{
    public partial class SeedAlgoProjectsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AlgoProjects",
                columns: new[] { "Id", "ItemName", "LongDescription", "Owner", "ShortDescription" },
                values: new object[,]
                {
                    { 1, "Item1", "It's a longdescription", "User1", "It's shortdescription" },
                    { 2, "Item2", "It's a longdescription", "User2", "It's shortdescription" },
                    { 3, "DANN", "It's a longdescription", "User1", "It's shortdescription" },
                    { 4, "AjaxTest", "It's a longdescription", "User1", "It's shortdescription" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AlgoProjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AlgoProjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AlgoProjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AlgoProjects",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
