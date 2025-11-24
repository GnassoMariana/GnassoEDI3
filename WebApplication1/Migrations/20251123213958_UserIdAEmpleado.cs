using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GnassoEDI3.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class UserIdAEmpleado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Empleados_EmpleadoId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_EmpleadoId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EmpleadoId",
                table: "User");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Empleados",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_UserId",
                table: "Empleados",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Empleados_User_UserId",
                table: "Empleados",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Empleados_User_UserId",
                table: "Empleados");

            migrationBuilder.DropIndex(
                name: "IX_Empleados_UserId",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Empleados");

            migrationBuilder.AddColumn<int>(
                name: "EmpleadoId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_EmpleadoId",
                table: "User",
                column: "EmpleadoId",
                unique: true,
                filter: "[EmpleadoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Empleados_EmpleadoId",
                table: "User",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
