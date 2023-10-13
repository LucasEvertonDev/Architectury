using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Architecture.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapPermissoesPorGrupoUsuario_GrupoUsuarios_GrupoUsuarioId",
                table: "MapPermissoesPorGrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_MapPermissoesPorGrupoUsuario_Permissoes_PermissaoId",
                table: "MapPermissoesPorGrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_GrupoUsuarios_GrupoUsuarioId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapPermissoesPorGrupoUsuario",
                table: "MapPermissoesPorGrupoUsuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GrupoUsuarios",
                table: "GrupoUsuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CredenciaisCliente",
                table: "CredenciaisCliente");

            migrationBuilder.RenameTable(
                name: "MapPermissoesPorGrupoUsuario",
                newName: "MapPermissoesPorGruposUsuarios");

            migrationBuilder.RenameTable(
                name: "GrupoUsuarios",
                newName: "GruposUsuarios");

            migrationBuilder.RenameTable(
                name: "CredenciaisCliente",
                newName: "CredenciaisClientes");

            migrationBuilder.RenameIndex(
                name: "IX_MapPermissoesPorGrupoUsuario_PermissaoId",
                table: "MapPermissoesPorGruposUsuarios",
                newName: "IX_MapPermissoesPorGruposUsuarios_PermissaoId");

            migrationBuilder.RenameIndex(
                name: "IX_MapPermissoesPorGrupoUsuario_GrupoUsuarioId",
                table: "MapPermissoesPorGruposUsuarios",
                newName: "IX_MapPermissoesPorGruposUsuarios_GrupoUsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapPermissoesPorGruposUsuarios",
                table: "MapPermissoesPorGruposUsuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GruposUsuarios",
                table: "GruposUsuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CredenciaisClientes",
                table: "CredenciaisClientes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CredenciaisClientes",
                keyColumn: "Id",
                keyValue: new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                column: "CreateDate",
                value: new DateTime(2023, 10, 13, 10, 44, 26, 222, DateTimeKind.Local).AddTicks(9072));

            migrationBuilder.AddForeignKey(
                name: "FK_MapPermissoesPorGruposUsuarios_GruposUsuarios_GrupoUsuarioId",
                table: "MapPermissoesPorGruposUsuarios",
                column: "GrupoUsuarioId",
                principalTable: "GruposUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapPermissoesPorGruposUsuarios_Permissoes_PermissaoId",
                table: "MapPermissoesPorGruposUsuarios",
                column: "PermissaoId",
                principalTable: "Permissoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_GruposUsuarios_GrupoUsuarioId",
                table: "Usuarios",
                column: "GrupoUsuarioId",
                principalTable: "GruposUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapPermissoesPorGruposUsuarios_GruposUsuarios_GrupoUsuarioId",
                table: "MapPermissoesPorGruposUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_MapPermissoesPorGruposUsuarios_Permissoes_PermissaoId",
                table: "MapPermissoesPorGruposUsuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_GruposUsuarios_GrupoUsuarioId",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MapPermissoesPorGruposUsuarios",
                table: "MapPermissoesPorGruposUsuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GruposUsuarios",
                table: "GruposUsuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CredenciaisClientes",
                table: "CredenciaisClientes");

            migrationBuilder.RenameTable(
                name: "MapPermissoesPorGruposUsuarios",
                newName: "MapPermissoesPorGrupoUsuario");

            migrationBuilder.RenameTable(
                name: "GruposUsuarios",
                newName: "GrupoUsuarios");

            migrationBuilder.RenameTable(
                name: "CredenciaisClientes",
                newName: "CredenciaisCliente");

            migrationBuilder.RenameIndex(
                name: "IX_MapPermissoesPorGruposUsuarios_PermissaoId",
                table: "MapPermissoesPorGrupoUsuario",
                newName: "IX_MapPermissoesPorGrupoUsuario_PermissaoId");

            migrationBuilder.RenameIndex(
                name: "IX_MapPermissoesPorGruposUsuarios_GrupoUsuarioId",
                table: "MapPermissoesPorGrupoUsuario",
                newName: "IX_MapPermissoesPorGrupoUsuario_GrupoUsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MapPermissoesPorGrupoUsuario",
                table: "MapPermissoesPorGrupoUsuario",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GrupoUsuarios",
                table: "GrupoUsuarios",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CredenciaisCliente",
                table: "CredenciaisCliente",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "CredenciaisCliente",
                keyColumn: "Id",
                keyValue: new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"),
                column: "CreateDate",
                value: new DateTime(2023, 10, 13, 10, 40, 4, 443, DateTimeKind.Local).AddTicks(3652));

            migrationBuilder.AddForeignKey(
                name: "FK_MapPermissoesPorGrupoUsuario_GrupoUsuarios_GrupoUsuarioId",
                table: "MapPermissoesPorGrupoUsuario",
                column: "GrupoUsuarioId",
                principalTable: "GrupoUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MapPermissoesPorGrupoUsuario_Permissoes_PermissaoId",
                table: "MapPermissoesPorGrupoUsuario",
                column: "PermissaoId",
                principalTable: "Permissoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_GrupoUsuarios_GrupoUsuarioId",
                table: "Usuarios",
                column: "GrupoUsuarioId",
                principalTable: "GrupoUsuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
