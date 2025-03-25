using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMSDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editMedicalRecordtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecordTests_LabTechnicians_LabTechnicianId",
                table: "MedicalRecordTests");

            migrationBuilder.DropColumn(
                name: "ExecutedBy",
                table: "MedicalRecordTests");

            migrationBuilder.AlterColumn<string>(
                name: "LabTechnicianId",
                table: "MedicalRecordTests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecordTests_MedicalRecordId",
                table: "MedicalRecordTests",
                column: "MedicalRecordId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecordTests_LabTechnicians_LabTechnicianId",
                table: "MedicalRecordTests",
                column: "LabTechnicianId",
                principalTable: "LabTechnicians",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecordTests_MedicalRecord_MedicalRecordId",
                table: "MedicalRecordTests",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecordTests_Tests_TestId",
                table: "MedicalRecordTests",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecordTests_LabTechnicians_LabTechnicianId",
                table: "MedicalRecordTests");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecordTests_MedicalRecord_MedicalRecordId",
                table: "MedicalRecordTests");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecordTests_Tests_TestId",
                table: "MedicalRecordTests");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecordTests_MedicalRecordId",
                table: "MedicalRecordTests");

            migrationBuilder.AlterColumn<string>(
                name: "LabTechnicianId",
                table: "MedicalRecordTests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExecutedBy",
                table: "MedicalRecordTests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecordTests_LabTechnicians_LabTechnicianId",
                table: "MedicalRecordTests",
                column: "LabTechnicianId",
                principalTable: "LabTechnicians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
