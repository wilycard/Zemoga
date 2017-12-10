using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MVCBankTransactions.Data.Migrations
{
    public partial class updateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ID",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "ID",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
