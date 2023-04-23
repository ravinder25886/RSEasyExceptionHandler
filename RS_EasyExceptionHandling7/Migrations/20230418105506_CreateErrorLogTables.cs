using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RS_EasyExceptionHandling7.Migrations
{
    /// <inheritdoc />
    public partial class CreateErrorLogTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAppErrorLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ErrorTitle = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    ErrorDetail = table.Column<string>(type: "TEXT", nullable: false),
                    ErrorSource = table.Column<string>(type: "TEXT", nullable: false),
                    ErrorDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    LogType = table.Column<string>(type: "TEXT", nullable: false),
                    ErrorCount = table.Column<int>(type: "INTEGER", nullable: false),
                    MailSentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsMailSent = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppErrorLogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblAppErrorLogs");
        }
    }
}
