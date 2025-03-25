using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMSDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addnullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DispinsedOn",
                table: "Prescriptions",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "DispinsedById",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "MedicalRecordEntityId",
                table: "MedicalRecordTests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecordTests_MedicalRecordEntityId",
                table: "MedicalRecordTests",
                column: "MedicalRecordEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalRecordTests_MedicalRecord_MedicalRecordEntityId",
                table: "MedicalRecordTests",
                column: "MedicalRecordEntityId",
                principalTable: "MedicalRecord",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalRecordTests_MedicalRecord_MedicalRecordEntityId",
                table: "MedicalRecordTests");

            migrationBuilder.DropIndex(
                name: "IX_MedicalRecordTests_MedicalRecordEntityId",
                table: "MedicalRecordTests");

            migrationBuilder.DropColumn(
                name: "MedicalRecordEntityId",
                table: "MedicalRecordTests");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DispinsedOn",
                table: "Prescriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DispinsedById",
                table: "Prescriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
