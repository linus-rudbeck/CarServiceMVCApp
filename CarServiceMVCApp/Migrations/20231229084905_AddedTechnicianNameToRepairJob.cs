using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarServiceMVCApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedTechnicianNameToRepairJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TechnicianName",
                table: "RepairJobs",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TechnicianName",
                table: "RepairJobs");
        }
    }
}
