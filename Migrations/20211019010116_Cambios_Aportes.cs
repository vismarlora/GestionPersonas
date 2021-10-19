using Microsoft.EntityFrameworkCore.Migrations;

namespace GestionPersonas.Migrations
{
    public partial class Cambios_Aportes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "TotalAportado",
                table: "Personas",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.CreateIndex(
                name: "IX_GruposDetalle_PersonaId",
                table: "GruposDetalle",
                column: "PersonaId");

            migrationBuilder.AddForeignKey(
                name: "FK_GruposDetalle_Personas_PersonaId",
                table: "GruposDetalle",
                column: "PersonaId",
                principalTable: "Personas",
                principalColumn: "PersonaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GruposDetalle_Personas_PersonaId",
                table: "GruposDetalle");

            migrationBuilder.DropIndex(
                name: "IX_GruposDetalle_PersonaId",
                table: "GruposDetalle");

            migrationBuilder.DropColumn(
                name: "TotalAportado",
                table: "Personas");
        }
    }
}
