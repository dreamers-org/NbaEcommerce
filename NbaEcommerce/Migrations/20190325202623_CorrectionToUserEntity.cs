using Microsoft.EntityFrameworkCore.Migrations;

namespace NbaEcommerce.Migrations
{
    public partial class CorrectionToUserEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CapSedeOperativa",
                table: "AspNetUsers",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CapSedeOperativa",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
