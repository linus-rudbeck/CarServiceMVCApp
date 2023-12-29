using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarServiceMVCApp.Migrations
{
    /// <inheritdoc />
    public partial class AddedRepairJobModelToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairJob_Cars_CarId",
                table: "RepairJob");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairJob",
                table: "RepairJob");

            migrationBuilder.RenameTable(
                name: "RepairJob",
                newName: "RepairJobs");

            migrationBuilder.RenameIndex(
                name: "IX_RepairJob_CarId",
                table: "RepairJobs",
                newName: "IX_RepairJobs_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairJobs",
                table: "RepairJobs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairJobs_Cars_CarId",
                table: "RepairJobs",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RepairJobs_Cars_CarId",
                table: "RepairJobs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RepairJobs",
                table: "RepairJobs");

            migrationBuilder.RenameTable(
                name: "RepairJobs",
                newName: "RepairJob");

            migrationBuilder.RenameIndex(
                name: "IX_RepairJobs_CarId",
                table: "RepairJob",
                newName: "IX_RepairJob_CarId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RepairJob",
                table: "RepairJob",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairJob_Cars_CarId",
                table: "RepairJob",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
