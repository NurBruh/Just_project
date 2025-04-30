using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Just_project.Migrations
{
    /// <inheritdoc />
    public partial class lkdfkhbfk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImagePath",
                table: "Pcs",
                type: "longblob",
                nullable: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "ImagePath",
                table: "Components",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Pcs");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Components");
        }
    }
}
