using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC.RecipeLogger.Migrations
{
    /// <inheritdoc />
    public partial class addnamefieldtorecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Recipes");
        }
    }
}
