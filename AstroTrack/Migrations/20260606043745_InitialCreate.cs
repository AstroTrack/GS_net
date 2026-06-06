using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AstroTrack.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AT_CLIENTES",
                columns: table => new
                {
                    ID_CLIENTE = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    CNPJ = table.Column<string>(type: "VARCHAR2(18)", nullable: false),
                    EMAIL = table.Column<string>(type: "VARCHAR2(120)", nullable: false),
                    TELEFONE = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    STATUS = table.Column<string>(type: "VARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT_CLIENTES", x => x.ID_CLIENTE);
                });

            migrationBuilder.CreateTable(
                name: "AT_MOTORISTAS",
                columns: table => new
                {
                    ID_MOTORISTA = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NOME = table.Column<string>(type: "VARCHAR2(100)", nullable: false),
                    CPF = table.Column<string>(type: "VARCHAR2(14)", nullable: false),
                    CNH = table.Column<string>(type: "VARCHAR2(11)", nullable: false),
                    TELEFONE = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    STATUS = table.Column<string>(type: "VARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT_MOTORISTAS", x => x.ID_MOTORISTA);
                });

            migrationBuilder.CreateTable(
                name: "AT_USUARIOS_SISTEMA",
                columns: table => new
                {
                    EMAIL = table.Column<string>(type: "VARCHAR2(120)", nullable: false),
                    USUARIO = table.Column<string>(type: "VARCHAR2(60)", nullable: false),
                    SENHA = table.Column<string>(type: "VARCHAR2(255)", nullable: false),
                    STATUS = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    DATA_CRIACAO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT_USUARIOS_SISTEMA", x => x.EMAIL);
                });

            migrationBuilder.CreateTable(
                name: "AT_VEICULOS",
                columns: table => new
                {
                    ID_VEICULO = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PLACA = table.Column<string>(type: "VARCHAR2(7)", nullable: false),
                    MODELO = table.Column<string>(type: "VARCHAR2(80)", nullable: false),
                    MARCA = table.Column<string>(type: "VARCHAR2(60)", nullable: false),
                    ANO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STATUS = table.Column<string>(type: "VARCHAR2(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT_VEICULOS", x => x.ID_VEICULO);
                });

            migrationBuilder.CreateTable(
                name: "AT_VIAGENS",
                columns: table => new
                {
                    ID_VIAGEM = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_CLIENTE = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_MOTORISTA = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ID_VEICULO = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    ORIGEM = table.Column<string>(type: "VARCHAR2(120)", nullable: false),
                    DESTINO = table.Column<string>(type: "VARCHAR2(120)", nullable: false),
                    DATA_INICIO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DATA_FIM = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    STATUS = table.Column<string>(type: "VARCHAR2(20)", nullable: false),
                    QUILOMETRAGEM_TOTAL = table.Column<decimal>(type: "NUMBER(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT_VIAGENS", x => x.ID_VIAGEM);
                    table.ForeignKey(
                        name: "FK_AT_VIAGENS_AT_CLIENTES_ID_CLIENTE",
                        column: x => x.ID_CLIENTE,
                        principalTable: "AT_CLIENTES",
                        principalColumn: "ID_CLIENTE",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AT_VIAGENS_AT_MOTORISTAS_ID_MOTORISTA",
                        column: x => x.ID_MOTORISTA,
                        principalTable: "AT_MOTORISTAS",
                        principalColumn: "ID_MOTORISTA",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AT_VIAGENS_AT_VEICULOS_ID_VEICULO",
                        column: x => x.ID_VEICULO,
                        principalTable: "AT_VEICULOS",
                        principalColumn: "ID_VEICULO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AT_CHECKPOINTS",
                columns: table => new
                {
                    ID_CHECKPOINT = table.Column<long>(type: "NUMBER(19)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_VIAGEM = table.Column<long>(type: "NUMBER(19)", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "NUMBER(10,7)", nullable: false),
                    LONGITUDE = table.Column<decimal>(type: "NUMBER(10,7)", nullable: false),
                    DATA_REGISTRO = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    BOTAO_PANICO = table.Column<bool>(type: "NUMBER(1)", nullable: false),
                    PORTA_ABERTA = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AT_CHECKPOINTS", x => x.ID_CHECKPOINT);
                    table.ForeignKey(
                        name: "FK_AT_CHECKPOINTS_AT_VIAGENS_ID_VIAGEM",
                        column: x => x.ID_VIAGEM,
                        principalTable: "AT_VIAGENS",
                        principalColumn: "ID_VIAGEM",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AT_CHECKPOINTS_ID_VIAGEM",
                table: "AT_CHECKPOINTS",
                column: "ID_VIAGEM");

            migrationBuilder.CreateIndex(
                name: "IX_AT_CLIENTES_CNPJ",
                table: "AT_CLIENTES",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AT_CLIENTES_EMAIL",
                table: "AT_CLIENTES",
                column: "EMAIL",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AT_MOTORISTAS_CNH",
                table: "AT_MOTORISTAS",
                column: "CNH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AT_MOTORISTAS_CPF",
                table: "AT_MOTORISTAS",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AT_VEICULOS_PLACA",
                table: "AT_VEICULOS",
                column: "PLACA",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AT_VIAGENS_ID_CLIENTE",
                table: "AT_VIAGENS",
                column: "ID_CLIENTE");

            migrationBuilder.CreateIndex(
                name: "IX_AT_VIAGENS_ID_MOTORISTA",
                table: "AT_VIAGENS",
                column: "ID_MOTORISTA");

            migrationBuilder.CreateIndex(
                name: "IX_AT_VIAGENS_ID_VEICULO",
                table: "AT_VIAGENS",
                column: "ID_VEICULO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AT_CHECKPOINTS");

            migrationBuilder.DropTable(
                name: "AT_USUARIOS_SISTEMA");

            migrationBuilder.DropTable(
                name: "AT_VIAGENS");

            migrationBuilder.DropTable(
                name: "AT_CLIENTES");

            migrationBuilder.DropTable(
                name: "AT_MOTORISTAS");

            migrationBuilder.DropTable(
                name: "AT_VEICULOS");
        }
    }
}
