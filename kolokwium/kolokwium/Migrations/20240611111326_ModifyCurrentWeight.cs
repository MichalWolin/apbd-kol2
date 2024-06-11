using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace kolokwium.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCurrentWeight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentWeight",
                value: 23);

            migrationBuilder.UpdateData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentWeight",
                value: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentWeight",
                value: 100);

            migrationBuilder.UpdateData(
                table: "characters",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentWeight",
                value: 50);
        }
    }
}
