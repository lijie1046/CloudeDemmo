using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudeDemmo.Migrations
{
    public partial class SeedDatasetsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Datasets",
                columns: new[] { "Id", "DataName", "LongDescription", "Owner", "ShortDescription" },
                values: new object[] { 1, "西储数据集", "无", "用户1", "凯斯西储大学轴承故障数据" });

            migrationBuilder.InsertData(
                table: "Datasets",
                columns: new[] { "Id", "DataName", "LongDescription", "Owner", "ShortDescription" },
                values: new object[] { 2, "数据2", "无", "用户1", "测试" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Datasets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Datasets",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
