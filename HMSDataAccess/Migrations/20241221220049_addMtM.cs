using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMSDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addMtM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SpecialtiesId",
                table: "DoctorSpecialties",
                newName: "SpecialtyId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSpecialties_SpecialtyId",
                table: "DoctorSpecialties",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialties_Doctors_DoctorId",
                table: "DoctorSpecialties",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialties_Specialties_SpecialtyId",
                table: "DoctorSpecialties",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialties_Doctors_DoctorId",
                table: "DoctorSpecialties");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialties_Specialties_SpecialtyId",
                table: "DoctorSpecialties");

            migrationBuilder.DropIndex(
                name: "IX_DoctorSpecialties_SpecialtyId",
                table: "DoctorSpecialties");

            migrationBuilder.RenameColumn(
                name: "SpecialtyId",
                table: "DoctorSpecialties",
                newName: "SpecialtiesId");
        }
    }
}
