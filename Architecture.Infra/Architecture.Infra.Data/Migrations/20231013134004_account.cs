using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Architecture.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class account : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LogradouroId",
                table: "Enderecos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PessoaId1",
                table: "Enderecos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CredenciaisCliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identificacao = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chave = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredenciaisCliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrupoUsuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUsuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logradouro",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logradouro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    GrupoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UltimoAcesso = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_GrupoUsuarios_GrupoUsuarioId",
                        column: x => x.GrupoUsuarioId,
                        principalTable: "GrupoUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rua",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogradouroId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Situacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rua", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rua_Logradouro_LogradouroId",
                        column: x => x.LogradouroId,
                        principalTable: "Logradouro",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MapPermissoesPorGrupoUsuario",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GrupoUsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Situacao = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapPermissoesPorGrupoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapPermissoesPorGrupoUsuario_GrupoUsuarios_GrupoUsuarioId",
                        column: x => x.GrupoUsuarioId,
                        principalTable: "GrupoUsuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MapPermissoesPorGrupoUsuario_Permissoes_PermissaoId",
                        column: x => x.PermissaoId,
                        principalTable: "Permissoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CredenciaisCliente",
                columns: new[] { "Id", "Chave", "CreateDate", "Descricao", "Identificacao", "Situacao", "UpdateDate" },
                values: new object[] { new Guid("9245fe4a-d402-451c-b9ed-9c1a04247482"), "dff0bcb8dad7ea803e8d28bf566bcd354b5ec4e96ff4576a1b71ec4a21d56910", new DateTime(2023, 10, 13, 10, 40, 4, 443, DateTimeKind.Local).AddTicks(3652), "Cliente padrão da aplicação", new Guid("7064bbbf-5d11-4782-9009-95e5a6fd6822"), 1, null });

            migrationBuilder.InsertData(
                table: "GrupoUsuarios",
                columns: new[] { "Id", "Descricao", "Nome", "Situacao" },
                values: new object[,]
                {
                    { new Guid("2c2ab8a3-3665-42ef-b4ef-bbec05ac02a5"), "Customer", "Customer", 1 },
                    { new Guid("f97e565d-08af-4281-bc11-c0206eae06fa"), "Admin", "Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "Permissoes",
                columns: new[] { "Id", "Nome", "Situacao" },
                values: new object[] { new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), "CHANGE_STUDENTS", 1 });

            migrationBuilder.InsertData(
                table: "MapPermissoesPorGrupoUsuario",
                columns: new[] { "Id", "GrupoUsuarioId", "PermissaoId", "Situacao" },
                values: new object[] { new Guid("b94afe49-6630-4bf8-a19d-923af259f475"), new Guid("f97e565d-08af-4281-bc11-c0206eae06fa"), new Guid("bbdbc055-b8b9-42af-b667-9a18c854ee8e"), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_LogradouroId",
                table: "Enderecos",
                column: "LogradouroId");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_PessoaId1",
                table: "Enderecos",
                column: "PessoaId1");

            migrationBuilder.CreateIndex(
                name: "IX_MapPermissoesPorGrupoUsuario_GrupoUsuarioId",
                table: "MapPermissoesPorGrupoUsuario",
                column: "GrupoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MapPermissoesPorGrupoUsuario_PermissaoId",
                table: "MapPermissoesPorGrupoUsuario",
                column: "PermissaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rua_LogradouroId",
                table: "Rua",
                column: "LogradouroId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_GrupoUsuarioId",
                table: "Usuarios",
                column: "GrupoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Logradouro_LogradouroId",
                table: "Enderecos",
                column: "LogradouroId",
                principalTable: "Logradouro",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Pessoas_PessoaId1",
                table: "Enderecos",
                column: "PessoaId1",
                principalTable: "Pessoas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Logradouro_LogradouroId",
                table: "Enderecos");

            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Pessoas_PessoaId1",
                table: "Enderecos");

            migrationBuilder.DropTable(
                name: "CredenciaisCliente");

            migrationBuilder.DropTable(
                name: "MapPermissoesPorGrupoUsuario");

            migrationBuilder.DropTable(
                name: "Rua");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Permissoes");

            migrationBuilder.DropTable(
                name: "Logradouro");

            migrationBuilder.DropTable(
                name: "GrupoUsuarios");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_LogradouroId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_PessoaId1",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "LogradouroId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "PessoaId1",
                table: "Enderecos");
        }
    }
}
