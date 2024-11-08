using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContenedorDB.Migrations
{
    /// <inheritdoc />
    public partial class user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
    name: "Usuario",
    columns: table => new
    {
        Id = table.Column<int>(type: "int", nullable: false)
            .Annotation("SqlServer:Identity", "1, 1"),
        Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
        Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
        Loggeado = table.Column<bool>(type: "bit", nullable: false),
        Pagado = table.Column<bool>(type: "bit", nullable: false)
    },
    constraints: table =>
    {
        table.PrimaryKey("PK_Usuario", x => x.Id);
    });




        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "Usuario");
        }
    }
}
