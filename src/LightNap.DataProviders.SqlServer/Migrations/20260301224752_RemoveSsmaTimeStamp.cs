using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNap.DataProviders.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSsmaTimeStamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSMA_TimeStamp",
                table: "Buletine_EDS");

            migrationBuilder.DropColumn(
                name: "SSMA_TimeStamp",
                table: "Buletine_EDI");

            migrationBuilder.DropColumn(
                name: "SSMA_TimeStamp",
                table: "Buletine_ECO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "SSMA_TimeStamp",
                table: "Buletine_EDS",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "SSMA_TimeStamp",
                table: "Buletine_EDI",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "SSMA_TimeStamp",
                table: "Buletine_ECO",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
