using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketApplication.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "UserTable",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "UserTable",
                newName: "PasswordHash");
        }
    }
}
