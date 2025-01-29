using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMSDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class FixName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "SartTime",
                table: "Appointments",
                newName: "StartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Appointments",
                newName: "SartTime");
        }
    }
}
