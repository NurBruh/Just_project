using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Just_project.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class pclocalizer1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Pcs",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Pcs");
        }
    }
}
