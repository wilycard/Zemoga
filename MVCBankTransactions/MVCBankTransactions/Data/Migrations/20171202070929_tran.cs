using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MVCBankTransactions.Data.Migrations
{
    public partial class tran : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    ID = table.Column<double>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    IsFlaggedFraud = table.Column<bool>(nullable: false),
                    IsFraud = table.Column<bool>(nullable: false),
                    NameDest = table.Column<string>(nullable: true),
                    NameOrig = table.Column<string>(nullable: true),
                    NewbalanceDest = table.Column<decimal>(nullable: false),
                    NewbalanceOrig = table.Column<decimal>(nullable: false),
                    OldbalanceDest = table.Column<decimal>(nullable: false),
                    OldbalanceOrig = table.Column<decimal>(nullable: false),
                    Step = table.Column<int>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
