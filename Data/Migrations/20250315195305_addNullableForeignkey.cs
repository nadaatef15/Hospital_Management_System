using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMSDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addNullableForeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmasistId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_PharmasistId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "PharmasistId",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<string>(
                name: "DispinsedById",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_DispinsedById",
                table: "Prescriptions",
                column: "DispinsedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacists_DispinsedById",
                table: "Prescriptions",
                column: "DispinsedById",
                principalTable: "Pharmacists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Pharmacists_DispinsedById",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_DispinsedById",
                table: "Prescriptions");

            migrationBuilder.AlterColumn<string>(
                name: "DispinsedById",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PharmasistId",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_PharmasistId",
                table: "Prescriptions",
                column: "PharmasistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Pharmacists_PharmasistId",
                table: "Prescriptions",
                column: "PharmasistId",
                principalTable: "Pharmacists",
                principalColumn: "Id");
        }
    }
}
