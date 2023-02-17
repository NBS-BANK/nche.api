using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VasMicroservices.NCHE.Infra.Data.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EndpointLogs",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RequestUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RawRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RawResponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: true),
                    Success = table.Column<int>(type: "int", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponseTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndpointLogs", x => x.RecordId);
                });

            migrationBuilder.CreateTable(
                name: "VasPayments",
                columns: table => new
                {
                    VasPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reversed = table.Column<int>(type: "int", nullable: true),
                    ReversedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NcheTxnRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NchestatusCode = table.Column<int>(type: "int", nullable: true),
                    RequestString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseString = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VasPayments", x => x.VasPaymentId);
                });

            migrationBuilder.CreateTable(
                name: "VasOfsLog",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VasPaymentId = table.Column<int>(type: "int", nullable: false),
                    OfscompanyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TellerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DebitAccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TxnRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ofssuccess = table.Column<byte>(type: "tinyint", nullable: true),
                    Ofsrequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ofsresponse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ofsmessage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VasOfsLog", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_VasOfsLog_VasPayments_VasPaymentId",
                        column: x => x.VasPaymentId,
                        principalTable: "VasPayments",
                        principalColumn: "VasPaymentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VasOfsLog_VasPaymentId",
                table: "VasOfsLog",
                column: "VasPaymentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndpointLogs");

            migrationBuilder.DropTable(
                name: "VasOfsLog");

            migrationBuilder.DropTable(
                name: "VasPayments");
        }
    }
}
