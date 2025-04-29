using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Just_project.Migrations
{
    /// <inheritdoc />
    public partial class bruha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImagePath",
                table: "Blogs",
                type: "longblob",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Blogs");
        }
    }
}
