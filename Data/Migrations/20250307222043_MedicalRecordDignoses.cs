using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMSDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class MedicalRecordDignoses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecordDiagnoses_DiagnosesId",
                table: "MedicalRecordDiagnoses",
                column: "DiagnosesId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecordDiagnoses_Diagnoses_DiagnosesId",
                table: "MedicalRecordDiagnoses",
                column: "DiagnosesId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecordDiagnoses_MedicalRecord_MedicalRecordId",
                table: "MedicalRecordDiagnoses",
                column: "MedicalRecordId",
                principalTable: "MedicalRecord",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecordDiagnoses_Diagnoses_DiagnosesId",
                table: "MedicalRecordDiagnoses");

            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecordDiagnoses_MedicalRecord_MedicalRecordId",
                table: "MedicalRecordDiagnoses");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecordDiagnoses_DiagnosesId",
                table: "MedicalRecordDiagnoses");
        }
    }
}
