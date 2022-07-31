using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestForVersta.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SenderAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ReceiverCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReceiverAddress = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.CheckConstraint("CK_Orders_DeliveryDates", "DATEDIFF(dd, '2020-01-01', [DeliveryDate]) >= 0");
                    table.CheckConstraint("CK_Orders_Weights", "[Weight] > 0");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
