using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Just_project.Migrations
{
    /// <inheritdoc />
    public partial class components1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ComponentsTranslations",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "Components",
                type: "float",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "ComponentsTranslations");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Components");
        }
    }
}
