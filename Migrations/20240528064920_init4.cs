using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketApplication.Migrations
{
    public partial class init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderTable",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    TicketQuantity = table.Column<int>(type: "int", nullable: false),
                    TicketPlan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTable", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_OrderTable_MatchTable_MatchID",
                        column: x => x.MatchID,
                        principalTable: "MatchTable",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTable_UserTable_UserID",
                        column: x => x.UserID,
                        principalTable: "UserTable",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_MatchID",
                table: "OrderTable",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTable_UserID",
                table: "OrderTable",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderTable");
        }
    }
}
