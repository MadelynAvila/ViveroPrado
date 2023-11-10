using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContenedorDB.Migrations
{
    /// <inheritdoc />
    public partial class MapeoColuma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Loggeado",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loggeado",
                table: "Usuario");
        }
    }
}
