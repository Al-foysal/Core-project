using Microsoft.EntityFrameworkCore.Migrations;

namespace M7_Class_37_Work_01.Migrations.CourseDb
{
    public partial class TradeChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Trades",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IndustrialAttachment",
                table: "Trades",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "IndustrialAttachment",
                table: "Trades");
        }
    }
}
