using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNap.DataProviders.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddMedicUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Medic",
                table: "Medici",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Medici",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medici_UserId",
                table: "Medici",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medici_AspNetUsers_UserId",
                table: "Medici",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medici_AspNetUsers_UserId",
                table: "Medici");

            migrationBuilder.DropIndex(
                name: "IX_Medici_UserId",
                table: "Medici");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Medici");

            migrationBuilder.DropColumn(
                name: "MedicName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Medic",
                table: "Medici",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
