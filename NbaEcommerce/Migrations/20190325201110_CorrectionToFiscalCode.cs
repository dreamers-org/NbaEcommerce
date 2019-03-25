using Microsoft.EntityFrameworkCore.Migrations;

namespace NbaEcommerce.Migrations
{
    public partial class CorrectionToFiscalCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodiceFiscale",
                table: "AspNetUsers",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 11);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CodiceFiscale",
                table: "AspNetUsers",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16);
        }
    }
}
