using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TPDetailing2.Migrations
{
    /// <inheritdoc />
    public partial class Angelito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turno_Empleado_EmpleadoId",
                table: "Turno");

            migrationBuilder.DropIndex(
                name: "IX_Turno_EmpleadoId",
                table: "Turno");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "Turno");

            migrationBuilder.RenameColumn(
                name: "Realizado",
                table: "Turno",
                newName: "Disponible");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Disponible",
                table: "Turno",
                newName: "Realizado");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "Turno",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Turno_EmpleadoId",
                table: "Turno",
                column: "EmpleadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turno_Empleado_EmpleadoId",
                table: "Turno",
                column: "EmpleadoId",
                principalTable: "Empleado",
                principalColumn: "UsuarioId");
        }
    }
}
