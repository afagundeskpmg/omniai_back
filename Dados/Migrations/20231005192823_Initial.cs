using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dados.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnexoArquivoTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    QuantidadeMaxima = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoArquivoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnexoTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArquivoFormato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    TamanhoMaximoMb = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoFormato", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Claim",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ClaimPaiId = table.Column<int>(type: "int", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    DefaultValue = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Claim_Claim_ClaimPaiId",
                        column: x => x.ClaimPaiId,
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClaimGrupo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClaimGrupo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmailDestinatarioTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDestinatarioTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InteracaoTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteracaoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentoPadrao = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CultureInfo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Papel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Papel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PessoaTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    QueueNome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnexoArquivoTipoArquivoFormato",
                columns: table => new
                {
                    AnexoArquivoTipoId = table.Column<int>(type: "int", nullable: false),
                    ArquivoFormatoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoArquivoTipoArquivoFormato", x => new { x.AnexoArquivoTipoId, x.ArquivoFormatoId });
                    table.ForeignKey(
                        name: "FK_AnexoArquivoTipoArquivoFormato_AnexoArquivoTipo_AnexoArquivoTipoId",
                        column: x => x.AnexoArquivoTipoId,
                        principalTable: "AnexoArquivoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnexoArquivoTipoArquivoFormato_ArquivoFormato_ArquivoFormatoId",
                        column: x => x.ArquivoFormatoId,
                        principalTable: "ArquivoFormato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArquivoFormatoAssinatura",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArquivoFormatoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoFormatoAssinatura", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoFormatoAssinatura_ArquivoFormato_ArquivoFormatoId",
                        column: x => x.ArquivoFormatoId,
                        principalTable: "ArquivoFormato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RelacaoClaimGrupo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    ClaimGrupoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelacaoClaimGrupo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RelacaoClaimGrupo_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelacaoClaimGrupo_ClaimGrupo_ClaimGrupoId",
                        column: x => x.ClaimGrupoId,
                        principalTable: "ClaimGrupo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PapelAcessivel",
                columns: table => new
                {
                    PapelId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    PapelAcessanteId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PapelAcessivel", x => new { x.PapelId, x.PapelAcessanteId });
                    table.ForeignKey(
                        name: "FK_PapelAcessivel_Papel_PapelAcessanteId",
                        column: x => x.PapelAcessanteId,
                        principalTable: "Papel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PapelAcessivel_Papel_PapelId",
                        column: x => x.PapelId,
                        principalTable: "Papel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PapelClaim",
                columns: table => new
                {
                    PapelId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PapelClaim", x => new { x.PapelId, x.ClaimId });
                    table.ForeignKey(
                        name: "FK_PapelClaim_Claim_ClaimId",
                        column: x => x.ClaimId,
                        principalTable: "Claim",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PapelClaim_Papel_PapelId",
                        column: x => x.PapelId,
                        principalTable: "Papel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LockoutEndDateUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    UltimoAcessoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PapelId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Federado = table.Column<bool>(type: "bit", nullable: false),
                    EmailBoasVindasEnviado = table.Column<bool>(type: "bit", nullable: false),
                    IdentityIdGeradoLocalmente = table.Column<bool>(type: "bit", nullable: false),
                    IdentityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UltimoAcessoIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Papel_PapelId",
                        column: x => x.PapelId,
                        principalTable: "Papel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Usuario_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Mascara = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false),
                    PessoaTipoId = table.Column<int>(type: "int", nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documento_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Documento_PessoaTipo_PessoaTipoId",
                        column: x => x.PessoaTipoId,
                        principalTable: "PessoaTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ArquivoFormatoAssinaturaByte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ArquivoFormatoAssinaturaId = table.Column<int>(type: "int", nullable: false),
                    Byte = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArquivoFormatoAssinaturaByte", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ArquivoFormatoAssinaturaByte_ArquivoFormatoAssinatura_ArquivoFormatoAssinaturaId",
                        column: x => x.ArquivoFormatoAssinaturaId,
                        principalTable: "ArquivoFormatoAssinatura",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Anexo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeArquivoAlterado = table.Column<string>(type: "nvarchar(68)", maxLength: 68, nullable: false),
                    NomeArquivoOriginal = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CaminhoArquivoBlobStorage = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    BlobContainerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AnexoTipoId = table.Column<int>(type: "int", nullable: false),
                    AnexoArquivoTipoId = table.Column<int>(type: "int", nullable: false),
                    ArquivoFormatoId = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Anexo_AnexoArquivoTipo_AnexoArquivoTipoId",
                        column: x => x.AnexoArquivoTipoId,
                        principalTable: "AnexoArquivoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_AnexoTipo_AnexoTipoId",
                        column: x => x.AnexoTipoId,
                        principalTable: "AnexoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_ArquivoFormato_ArquivoFormatoId",
                        column: x => x.ArquivoFormatoId,
                        principalTable: "ArquivoFormato",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Anexo_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Interacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false),
                    InteracaoTipoId = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interacao_InteracaoTipo_InteracaoTipoId",
                        column: x => x.InteracaoTipoId,
                        principalTable: "InteracaoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interacao_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Interacao_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Processamento",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    QueueMessageId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    QueueExpiraEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FimProcessamentoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InicioProcessamentoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessamentoStatusId = table.Column<int>(type: "int", nullable: false),
                    ProcessamentoTipoId = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processamento_ProcessamentoStatus_ProcessamentoStatusId",
                        column: x => x.ProcessamentoStatusId,
                        principalTable: "ProcessamentoStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processamento_ProcessamentoTipo_ProcessamentoTipoId",
                        column: x => x.ProcessamentoTipoId,
                        principalTable: "ProcessamentoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processamento_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Processamento_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TemaConfiguracao",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DarkMode = table.Column<bool>(type: "bit", nullable: false),
                    Layout = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LogoBg = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    NavbarBg = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SidebarType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SidebarColor = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Components = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    SidebarPosition = table.Column<bool>(type: "bit", nullable: false),
                    HeaderPosition = table.Column<bool>(type: "bit", nullable: false),
                    BoxedLayout = table.Column<bool>(type: "bit", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemaConfiguracao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemaConfiguracao_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserClaim",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ClaimValue = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaim", x => new { x.UsuarioId, x.ClaimType, x.ClaimValue });
                    table.ForeignKey(
                        name: "FK_UserClaim_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogin",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioLogin_Usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: false),
                    PaisId = table.Column<int>(type: "int", nullable: false),
                    PessoaTipoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Documento = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DocumentoTipoId = table.Column<int>(type: "int", nullable: true),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoa_Documento_DocumentoTipoId",
                        column: x => x.DocumentoTipoId,
                        principalTable: "Documento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_Pais_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Pais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_PessoaTipo_PessoaTipoId",
                        column: x => x.PessoaTipoId,
                        principalTable: "PessoaTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pessoa_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Assunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorpoAnexoId = table.Column<int>(type: "int", nullable: false),
                    RemetenteEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EnviadoEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Email_Anexo_CorpoAnexoId",
                        column: x => x.CorpoAnexoId,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InteracaoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    AcessoIP = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteracaoUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteracaoUsuario_Interacao_Id",
                        column: x => x.Id,
                        principalTable: "Interacao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InteracaoUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessamentoId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessamentoLog_Processamento_ProcessamentoId",
                        column: x => x.ProcessamentoId,
                        principalTable: "Processamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaJuridica",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    DadosCadastrais = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaJuridica", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PessoaJuridica_Pessoa_Id",
                        column: x => x.Id,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnexoEmail",
                columns: table => new
                {
                    AnexoId = table.Column<int>(type: "int", nullable: false),
                    EmailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoEmail", x => new { x.AnexoId, x.EmailId });
                    table.ForeignKey(
                        name: "FK_AnexoEmail_Anexo_AnexoId",
                        column: x => x.AnexoId,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnexoEmail_Email_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Email",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailDestinatario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailDestinatarioTipoId = table.Column<int>(type: "int", nullable: false),
                    EmailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDestinatario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailDestinatario_Email_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Email",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailDestinatario_EmailDestinatarioTipo_EmailDestinatarioTipoId",
                        column: x => x.EmailDestinatarioTipoId,
                        principalTable: "EmailDestinatarioTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoEmail",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    EmailId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoEmail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessamentoEmail_Email_EmailId",
                        column: x => x.EmailId,
                        principalTable: "Email",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessamentoEmail_Processamento_Id",
                        column: x => x.Id,
                        principalTable: "Processamento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ambiente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClienteId = table.Column<int>(type: "int", nullable: false),
                    CongnitiveSearchSize = table.Column<int>(type: "int", nullable: false),
                    CognitiveSearchIndexName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CognitiveSearchSkillSetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LimiteTokenPorRequisicao = table.Column<int>(type: "int", nullable: false),
                    NumeroEntidades = table.Column<int>(type: "int", nullable: true),
                    QuantidadeMaximaUsuariosAtivos = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ambiente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ambiente_PessoaJuridica_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "PessoaJuridica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ambiente_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ambiente_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailDestinatarioUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailDestinatarioUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailDestinatarioUsuario_EmailDestinatario_Id",
                        column: x => x.Id,
                        principalTable: "EmailDestinatario",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmailDestinatarioUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DocumentoTipo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AmbienteId = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentoTipo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentoTipo_Ambiente_AmbienteId",
                        column: x => x.AmbienteId,
                        principalTable: "Ambiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentoTipo_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentoTipo_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InteracaoAmbiente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    AmbienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InteracaoAmbiente", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InteracaoAmbiente_Ambiente_AmbienteId",
                        column: x => x.AmbienteId,
                        principalTable: "Ambiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InteracaoAmbiente_Interacao_Id",
                        column: x => x.Id,
                        principalTable: "Interacao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projeto",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AmbienteId = table.Column<int>(type: "int", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projeto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Projeto_Ambiente_AmbienteId",
                        column: x => x.AmbienteId,
                        principalTable: "Ambiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projeto_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Projeto_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioAmbiente",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    AmbienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioAmbiente", x => new { x.UsuarioId, x.AmbienteId });
                    table.ForeignKey(
                        name: "FK_UsuarioAmbiente_Ambiente_AmbienteId",
                        column: x => x.AmbienteId,
                        principalTable: "Ambiente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioAmbiente_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entidade",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Pergunta = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Query = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    Dados = table.Column<string>(type: "varchar(max)", nullable: false),
                    DocumentoTipoId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CriadoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    UltimaAlteracaoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UltimaAlteracaoPorId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Excluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entidade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entidade_DocumentoTipo_DocumentoTipoId",
                        column: x => x.DocumentoTipoId,
                        principalTable: "DocumentoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entidade_Usuario_CriadoPorId",
                        column: x => x.CriadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Entidade_Usuario_UltimaAlteracaoPorId",
                        column: x => x.UltimaAlteracaoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PerguntaResposta",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjetoId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    Prompt = table.Column<string>(type: "varchar(max)", nullable: true),
                    Pergunta = table.Column<string>(type: "varchar(max)", nullable: true),
                    Resposta = table.Column<string>(type: "varchar(max)", nullable: true),
                    TokensPergunta = table.Column<int>(type: "int", nullable: true),
                    TokensResposta = table.Column<int>(type: "int", nullable: true),
                    Dados = table.Column<string>(type: "varchar(max)", nullable: true),
                    CaminhoBlob = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerguntaResposta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerguntaResposta_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoIndexer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    InicioIndexacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FimIndexacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LiberadoEm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LiberadoPorId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    ProjetoId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    BlobFolder = table.Column<string>(type: "nvarchar(max)", maxLength: 8000, nullable: false),
                    Dados = table.Column<string>(type: "varchar(max)", nullable: true),
                    DataSourceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IndexerName = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DeletarIndexer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoIndexer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessamentoIndexer_Processamento_Id",
                        column: x => x.Id,
                        principalTable: "Processamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessamentoIndexer_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessamentoIndexer_Usuario_LiberadoPorId",
                        column: x => x.LiberadoPorId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoNer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjetoId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoNer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessamentoNer_Processamento_Id",
                        column: x => x.Id,
                        principalTable: "Processamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessamentoNer_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnexoPagina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    AnexoPaiId = table.Column<int>(type: "int", nullable: false),
                    AnexoId = table.Column<int>(type: "int", nullable: false),
                    PerguntaRespostaId = table.Column<string>(type: "nvarchar(36)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnexoPagina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnexoPagina_Anexo_AnexoId",
                        column: x => x.AnexoId,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnexoPagina_Anexo_AnexoPaiId",
                        column: x => x.AnexoPaiId,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnexoPagina_PerguntaResposta_PerguntaRespostaId",
                        column: x => x.PerguntaRespostaId,
                        principalTable: "PerguntaResposta",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjetoAnexo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnexoId = table.Column<int>(type: "int", nullable: false),
                    DocumentoTipoId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    ProjetoId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ProcessamentoIndexerId = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjetoAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjetoAnexo_Anexo_AnexoId",
                        column: x => x.AnexoId,
                        principalTable: "Anexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjetoAnexo_DocumentoTipo_DocumentoTipoId",
                        column: x => x.DocumentoTipoId,
                        principalTable: "DocumentoTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjetoAnexo_ProcessamentoIndexer_ProcessamentoIndexerId",
                        column: x => x.ProcessamentoIndexerId,
                        principalTable: "ProcessamentoIndexer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjetoAnexo_Projeto_ProjetoId",
                        column: x => x.ProjetoId,
                        principalTable: "Projeto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoAnexo",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjetoAnexoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoAnexo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessamentoAnexo_Processamento_Id",
                        column: x => x.Id,
                        principalTable: "Processamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessamentoAnexo_ProjetoAnexo_ProjetoAnexoId",
                        column: x => x.ProjetoAnexoId,
                        principalTable: "ProjetoAnexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessamentoPergunta",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    ProjetoAnexoId = table.Column<int>(type: "int", nullable: false),
                    ProcessamentoNerId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    PerguntaRespostaId = table.Column<string>(type: "nvarchar(36)", nullable: true),
                    EntidadeId = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    InicioConsulta = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FimConsulta = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessamentoPergunta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProcessamentoPergunta_Entidade_EntidadeId",
                        column: x => x.EntidadeId,
                        principalTable: "Entidade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessamentoPergunta_PerguntaResposta_PerguntaRespostaId",
                        column: x => x.PerguntaRespostaId,
                        principalTable: "PerguntaResposta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessamentoPergunta_Processamento_Id",
                        column: x => x.Id,
                        principalTable: "Processamento",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessamentoPergunta_ProcessamentoNer_ProcessamentoNerId",
                        column: x => x.ProcessamentoNerId,
                        principalTable: "ProcessamentoNer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProcessamentoPergunta_ProjetoAnexo_ProjetoAnexoId",
                        column: x => x.ProjetoAnexoId,
                        principalTable: "ProjetoAnexo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AnexoArquivoTipo",
                columns: new[] { "Id", "Nome", "QuantidadeMaxima" },
                values: new object[,]
                {
                    { 1, "Outros", 0 },
                    { 2, "Carga", 0 },
                    { 3, "Página HTML", 0 },
                    { 4, "Documento", 0 }
                });

            migrationBuilder.InsertData(
                table: "AnexoTipo",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Email Corpo" },
                    { 2, "Documento" },
                    { 3, "Documento Página" }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormato",
                columns: new[] { "Id", "MimeType", "Nome", "TamanhoMaximoMb" },
                values: new object[,]
                {
                    { 1, "audio/aac", ".aac", 5.0 },
                    { 2, "application/x-abiword", ".abw", 5.0 },
                    { 3, "application/octet-stream", ".arc", 5.0 },
                    { 4, "video/x-msvideo", ".avi", 5.0 },
                    { 5, "application/vnd.amazon.ebook", ".azw", 5.0 },
                    { 6, "application/octet-stream", ".bin", 5.0 },
                    { 7, "application/x-bzip", ".bz", 5.0 },
                    { 8, "application/x-bzip2", ".bz2", 5.0 },
                    { 9, "application/x-csh", ".csh", 5.0 },
                    { 10, "text/css", ".css", 5.0 },
                    { 11, "text/csv", ".csv", 150.0 },
                    { 12, "application/msword", ".doc", 5.0 },
                    { 13, "application/vnd.ms-fontobject", ".eot", 5.0 },
                    { 14, "application/epub+zip", ".epub", 5.0 },
                    { 15, "image/gif", ".gif", 5.0 },
                    { 16, "text/html", ".htm", 10.0 },
                    { 17, "text/html", ".html", 10.0 },
                    { 18, "image/x-icon", ".ico", 5.0 },
                    { 19, "text/calendar", ".ics", 5.0 },
                    { 20, "application/java-archive", ".jar", 5.0 },
                    { 21, "image/jpeg", ".jpeg", 5.0 },
                    { 22, "image/jpeg", ".jpg", 5.0 },
                    { 23, "application/javascript", ".js", 0.0 },
                    { 24, "application/json", ".json", 5.0 },
                    { 25, "audio/midi", ".mid", 5.0 },
                    { 26, "audio/midi", ".midi", 5.0 },
                    { 27, "video/mpeg", ".mpeg", 5.0 },
                    { 28, "application/vnd.apple.installer+xml", ".mpkg", 5.0 },
                    { 29, "application/vnd.oasis.opendocument.presentation", ".odp", 5.0 },
                    { 30, "application/vnd.oasis.opendocument.spreadsheet", ".ods", 5.0 },
                    { 31, "application/vnd.oasis.opendocument.text", ".odt", 5.0 },
                    { 32, "audio/ogg", ".oga", 5.0 },
                    { 33, "video/ogg", ".ogv", 5.0 },
                    { 34, "application/ogg", ".ogx", 5.0 },
                    { 35, "font/otf", ".otf", 5.0 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormato",
                columns: new[] { "Id", "MimeType", "Nome", "TamanhoMaximoMb" },
                values: new object[,]
                {
                    { 36, "image/png", ".png", 10.0 },
                    { 37, "application/pdf", ".pdf", 100.0 },
                    { 38, "application/vnd.ms-powerpoint", ".ppt", 5.0 },
                    { 39, "application/x-rar-compressed", ".rar", 5.0 },
                    { 40, "application/rtf", ".rtf", 5.0 },
                    { 41, "application/x-sh", ".sh", 5.0 },
                    { 42, "image/svg+xml", ".svg", 5.0 },
                    { 43, "application/x-shockwave-flash", ".swf", 5.0 },
                    { 44, "application/x-tar", ".tar", 5.0 },
                    { 45, "image/tiff", ".tif", 5.0 },
                    { 46, "image/tiff", ".tiff", 5.0 },
                    { 47, "application/typescript", ".ts", 5.0 },
                    { 48, "font/ttf", ".ttf", 5.0 },
                    { 49, "application/vnd.visio", ".vsd", 5.0 },
                    { 50, "audio/x-wav", ".wav", 5.0 },
                    { 51, "audio/webm", ".weba", 5.0 },
                    { 52, "video/webm", ".webm", 5.0 },
                    { 53, "image/webp", ".webp", 5.0 },
                    { 54, "font/woff", ".woff", 5.0 },
                    { 55, "font/woff2", ".woff2", 5.0 },
                    { 56, "application/xhtml+xml", ".xhtml", 5.0 },
                    { 57, "application/vnd.ms-excel", ".xls", 150.0 },
                    { 58, "application/vnd.ms-excel", ".xlsx", 150.0 },
                    { 59, "application/xml", ".xml", 5.0 },
                    { 60, "application/vnd.mozilla.xul+xml", ".xul", 5.0 },
                    { 61, "application/zip", ".zip", 80.0 },
                    { 62, "application/txt", ".txt", 10.0 },
                    { 63, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", ".docx", 5.0 }
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "ClaimPaiId", "ClaimType", "DefaultValue", "Descricao" },
                values: new object[,]
                {
                    { 1002, null, "VisualizarTodosAmbientes", "True", "Visualizar todos os ambientes" },
                    { 1004, null, "VisualizarMeusAmbientes", "True", "Visualizar meus ambientes" },
                    { 2002, null, "VisualizarTodosUsuarios", "True", "Visualizar todos os usuários" },
                    { 2004, null, "VisualizarMeusUsuarios", "True", "Visualizar meus usuários" },
                    { 3002, null, "VisualizarTodosProjetos", "True", "Visualizar todos os projetos" },
                    { 3004, null, "VisualizarMeusProjetos", "True", "Visualizar meus projetos" },
                    { 4002, null, "VisualizarTodosDocumentosTipo", "True", "Visualizar todos os tipos de documentos" },
                    { 4004, null, "VisualizarMeusDocumentosTipo", "True", "Visualizar meus tipos de documentos" },
                    { 5002, null, "VisualizarTodasEntidades", "True", "Visualizar todas as entidades" },
                    { 5004, null, "VisualizarMinhasEntidades", "True", "Visualizar minhas entidades" },
                    { 6002, null, "VisualizarTodasAnexos", "True", "Visualizar todos os anexos" },
                    { 6004, null, "VisualizarMeusAnexos", "True", "Visualizar meus anexos" },
                    { 6006, 6006, "VisualizarTodosAnexosAssociados", "True", "Visualizar todos anexos associados" },
                    { 7002, null, "VisualizarTodasProcessamentos", "True", "Visualizar todos os processamentos" }
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "ClaimPaiId", "ClaimType", "DefaultValue", "Descricao" },
                values: new object[] { 7004, null, "VisualizarMeusProcessamentos", "True", "Visualizar meus processamentos" });

            migrationBuilder.InsertData(
                table: "ClaimGrupo",
                columns: new[] { "Id", "Descricao", "Name" },
                values: new object[,]
                {
                    { 1, "Estas alçadas concedem permissões relacionadas ao gerenciamento dos ambientes da plataforma.", "Ambiente" },
                    { 2, "Estas alçadas concedem permissões relacionadas ao gerenciamento dos usuários da plataforma.", "Usuário" },
                    { 3, "Estas alçadas concedem permissões relacionadas ao gerenciamento dos projetos da plataforma.", "Projeto" },
                    { 4, "Estas alçadas concedem permissões relacionadas ao gerenciamento dos documentos da plataforma.", "Tipo de Documento" },
                    { 5, "Estas alçadas concedem permissões relacionadas ao gerenciamento dos entidades da plataforma.", "Entidade" },
                    { 6, "Estas alçadas concedem permissões relacionadas ao gerenciamento dos anexos da plataforma.", "Anexos" },
                    { 7, "Estas alçadas concedem permissões relacionadas ao gerenciamento dos processamentos da plataforma.", "Processamentos" }
                });

            migrationBuilder.InsertData(
                table: "EmailDestinatarioTipo",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Usuario" },
                    { 2, "Contato" }
                });

            migrationBuilder.InsertData(
                table: "InteracaoTipo",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Ambiente" },
                    { 2, "Usuario" },
                    { 3, "Projeto" }
                });

            migrationBuilder.InsertData(
                table: "Pais",
                columns: new[] { "Id", "CultureInfo", "DocumentoPadrao", "Flag", "Nome" },
                values: new object[,]
                {
                    { 1, "pt-BR", "CNPJ", "flag-icon-br", "Brasil" },
                    { 2, "es-ES", "", "flag-icon-ar", "Argentina" },
                    { 3, "es-ES", "", "flag-icon-bo", "Bolívia" },
                    { 4, "es-ES", "", "flag-icon-co", "Colômbia" },
                    { 5, "en-US", "", "flag-icon-gf", "Guiana" },
                    { 6, "es-ES", "", "flag-icon-py", "Paraguai" },
                    { 7, "es-ES", "RUC", "flag-icon-pe", "Peru" },
                    { 8, "en-US", "", "flag-icon-sr", "Suriname" },
                    { 9, "en-US", "", "flag-icon-uy", "Uruguai" },
                    { 10, "es-ES", "", "flag-icon-ve", "Venezuela" },
                    { 11, "en-US", "Registry ID", "flag-icon-ca", "Canada" }
                });

            migrationBuilder.InsertData(
                table: "Papel",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { "43e9fd1c-a4f2-4165-9cee-3edb4931f1af", "Sistema" },
                    { "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", "KPMG" },
                    { "8a55ed94-eece-4e7f-ab46-43185864b7d1", "Administrador" }
                });

            migrationBuilder.InsertData(
                table: "PessoaTipo",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Fisica" },
                    { 2, "Juridica" }
                });

            migrationBuilder.InsertData(
                table: "ProcessamentoStatus",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Solicitado" },
                    { 2, "Em Processamento" },
                    { 3, "Processado com Erro" },
                    { 4, "Processado com Sucesso" }
                });

            migrationBuilder.InsertData(
                table: "ProcessamentoTipo",
                columns: new[] { "Id", "Nome", "QueueNome" },
                values: new object[,]
                {
                    { 1, "Indexer", "processamentoindexer" },
                    { 2, "Email", "processamentoemail" },
                    { 3, "Anexo", "processamentoanexo" },
                    { 4, "Ner", "processamentoner" },
                    { 5, "Pergunta", "processamentopergunta" }
                });

            migrationBuilder.InsertData(
                table: "AnexoArquivoTipoArquivoFormato",
                columns: new[] { "AnexoArquivoTipoId", "ArquivoFormatoId" },
                values: new object[,]
                {
                    { 1, 11 },
                    { 1, 12 },
                    { 1, 15 },
                    { 1, 21 },
                    { 1, 22 },
                    { 1, 36 },
                    { 1, 37 },
                    { 1, 38 },
                    { 1, 57 },
                    { 1, 58 },
                    { 1, 61 },
                    { 1, 63 },
                    { 2, 11 },
                    { 2, 57 },
                    { 2, 58 },
                    { 2, 61 },
                    { 3, 16 },
                    { 4, 37 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinatura",
                columns: new[] { "Id", "ArquivoFormatoId" },
                values: new object[,]
                {
                    { 1, 37 },
                    { 2, 57 },
                    { 3, 57 },
                    { 4, 57 },
                    { 5, 57 },
                    { 6, 57 },
                    { 7, 57 },
                    { 8, 57 },
                    { 9, 57 },
                    { 10, 57 },
                    { 11, 58 },
                    { 12, 58 },
                    { 13, 63 },
                    { 14, 63 },
                    { 15, 12 },
                    { 16, 12 },
                    { 17, 12 },
                    { 18, 12 },
                    { 19, 12 },
                    { 20, 15 },
                    { 21, 18 },
                    { 22, 22 },
                    { 23, 22 },
                    { 24, 22 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinatura",
                columns: new[] { "Id", "ArquivoFormatoId" },
                values: new object[,]
                {
                    { 25, 21 },
                    { 26, 21 },
                    { 27, 21 },
                    { 28, 36 },
                    { 29, 38 },
                    { 30, 38 },
                    { 31, 38 },
                    { 32, 38 },
                    { 33, 38 },
                    { 34, 38 },
                    { 35, 38 },
                    { 36, 40 },
                    { 37, 45 },
                    { 38, 45 },
                    { 39, 45 },
                    { 40, 45 },
                    { 41, 46 },
                    { 42, 46 },
                    { 43, 46 },
                    { 44, 46 },
                    { 45, 59 },
                    { 46, 61 },
                    { 47, 61 },
                    { 48, 61 },
                    { 49, 61 },
                    { 50, 61 },
                    { 51, 61 },
                    { 52, 61 },
                    { 53, 3 },
                    { 54, 3 },
                    { 55, 3 },
                    { 56, 3 },
                    { 57, 3 },
                    { 58, 3 },
                    { 59, 4 },
                    { 60, 6 },
                    { 61, 8 },
                    { 62, 9 },
                    { 63, 20 },
                    { 64, 20 },
                    { 65, 20 },
                    { 66, 20 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinatura",
                columns: new[] { "Id", "ArquivoFormatoId" },
                values: new object[,]
                {
                    { 67, 21 },
                    { 68, 22 },
                    { 69, 29 },
                    { 70, 31 },
                    { 71, 32 },
                    { 72, 33 },
                    { 73, 34 },
                    { 74, 39 },
                    { 75, 43 },
                    { 76, 43 },
                    { 77, 44 },
                    { 78, 49 },
                    { 79, 50 }
                });

            migrationBuilder.InsertData(
                table: "Claim",
                columns: new[] { "Id", "ClaimPaiId", "ClaimType", "DefaultValue", "Descricao" },
                values: new object[,]
                {
                    { 1001, 1002, "AlterarTodosAmbientes", "True", "Alterar todos os ambientes" },
                    { 1003, 1004, "AlterarMeusAmbientes", "True", "Alterar meus ambientes" },
                    { 2001, 2002, "AlterarTodosUsuarios", "True", "Alterar todos os usuários" },
                    { 2003, 2004, "AlterarMeusUsuarios", "True", "Alterar meus usuários" },
                    { 3001, 3002, "AlterarTodosProjetos", "True", "Alterar todos os projetos" },
                    { 3003, 3004, "AlterarMeusProjetos", "True", "Alterar meus projetos" },
                    { 4001, 4002, "AlterarTodosDocumentosTipo", "True", "Alterar todos os tipos de documentos" },
                    { 4003, 4004, "AlterarMeusDocumentosTipo", "True", "Alterar meus tipos de documentos" },
                    { 5001, 5002, "AlterarTodosEntidades", "True", "Alterar todas as entidades" },
                    { 5003, 5004, "AlterarMinhasEntidades", "True", "Alterar minhas entidades" },
                    { 6001, 6002, "AlterarTodosAnexos", "True", "Alterar todos os anexos" },
                    { 6003, 6004, "AlterarMeusAnexos", "True", "Alterar meus anexos" },
                    { 6005, 6006, "AlterarTodosAnexosAssociados", "True", "Alterar todo anexos associados" },
                    { 7001, 7002, "AlterarTodosProcessamentos", "True", "Alterar todos os processamentos" },
                    { 7003, 7004, "AlterarMeusProcessamentos", "True", "Alterar meus processamentos" }
                });

            migrationBuilder.InsertData(
                table: "Documento",
                columns: new[] { "Id", "Mascara", "Nome", "PaisId", "PessoaTipoId", "Principal" },
                values: new object[,]
                {
                    { 1, "999.999.999-99", "CPF", 1, 1, true },
                    { 2, "00.000.000/0000-00", "CNPJ", 1, 2, true }
                });

            migrationBuilder.InsertData(
                table: "PapelAcessivel",
                columns: new[] { "PapelAcessanteId", "PapelId" },
                values: new object[,]
                {
                    { "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", "43e9fd1c-a4f2-4165-9cee-3edb4931f1af" },
                    { "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { "8a55ed94-eece-4e7f-ab46-43185864b7d1", "8a55ed94-eece-4e7f-ab46-43185864b7d1" }
                });

            migrationBuilder.InsertData(
                table: "PapelClaim",
                columns: new[] { "ClaimId", "PapelId" },
                values: new object[,]
                {
                    { 1002, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 1004, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 2002, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 2004, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 3002, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 3004, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 4002, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 4004, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" }
                });

            migrationBuilder.InsertData(
                table: "PapelClaim",
                columns: new[] { "ClaimId", "PapelId" },
                values: new object[,]
                {
                    { 5002, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 5004, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 6002, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 6004, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 6006, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 7002, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 7004, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 1004, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 2004, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 3004, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 4004, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 5004, "8a55ed94-eece-4e7f-ab46-43185864b7d1" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "AccessFailedCount", "CriadoEm", "CriadoPorId", "EmailBoasVindasEnviado", "Excluido", "Federado", "IdentityId", "IdentityIdGeradoLocalmente", "LockoutEnabled", "LockoutEndDateUtc", "Nome", "PapelId", "UltimaAlteracaoEm", "UltimaAlteracaoPorId", "UltimoAcessoEm", "UltimoAcessoIP", "UserName" },
                values: new object[] { "f60753d7-c5a7-4496-a360-c1a301d87763", 0, null, null, false, false, false, "f60753d7-c5a7-4496-a360-c1a301d87763", false, false, null, "Usuário de integração", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", null, null, null, null, "integracao@watch.kpmg.com.br" });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 1, 1, (byte)37 },
                    { 2, 1, (byte)80 },
                    { 3, 1, (byte)68 },
                    { 4, 1, (byte)70 },
                    { 5, 2, (byte)208 },
                    { 6, 2, (byte)207 },
                    { 7, 2, (byte)17 },
                    { 8, 2, (byte)224 },
                    { 9, 3, (byte)208 },
                    { 10, 3, (byte)207 },
                    { 11, 3, (byte)17 },
                    { 12, 3, (byte)224 },
                    { 13, 3, (byte)161 },
                    { 14, 3, (byte)177 },
                    { 15, 3, (byte)26 },
                    { 16, 3, (byte)225 },
                    { 17, 4, (byte)9 },
                    { 18, 4, (byte)8 },
                    { 19, 4, (byte)16 },
                    { 20, 4, (byte)0 },
                    { 21, 4, (byte)0 },
                    { 22, 4, (byte)6 },
                    { 23, 4, (byte)5 },
                    { 24, 4, (byte)0 },
                    { 25, 5, (byte)253 },
                    { 26, 5, (byte)255 },
                    { 27, 5, (byte)255 },
                    { 28, 5, (byte)255 },
                    { 29, 5, (byte)16 },
                    { 30, 6, (byte)253 },
                    { 31, 6, (byte)255 },
                    { 32, 6, (byte)255 },
                    { 33, 6, (byte)255 },
                    { 34, 6, (byte)31 },
                    { 35, 7, (byte)253 },
                    { 36, 7, (byte)255 },
                    { 37, 7, (byte)255 },
                    { 38, 7, (byte)255 },
                    { 39, 7, (byte)34 },
                    { 40, 8, (byte)253 },
                    { 41, 8, (byte)255 },
                    { 42, 8, (byte)255 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 43, 8, (byte)255 },
                    { 44, 8, (byte)35 },
                    { 45, 9, (byte)253 },
                    { 46, 9, (byte)255 },
                    { 47, 9, (byte)255 },
                    { 48, 9, (byte)255 },
                    { 49, 9, (byte)40 },
                    { 50, 10, (byte)253 },
                    { 51, 10, (byte)255 },
                    { 52, 10, (byte)255 },
                    { 53, 10, (byte)255 },
                    { 54, 10, (byte)41 },
                    { 55, 11, (byte)80 },
                    { 56, 11, (byte)75 },
                    { 57, 11, (byte)3 },
                    { 58, 11, (byte)4 },
                    { 59, 12, (byte)80 },
                    { 60, 12, (byte)75 },
                    { 61, 12, (byte)3 },
                    { 62, 12, (byte)4 },
                    { 63, 12, (byte)20 },
                    { 64, 12, (byte)0 },
                    { 65, 12, (byte)6 },
                    { 66, 12, (byte)0 },
                    { 67, 13, (byte)80 },
                    { 68, 13, (byte)75 },
                    { 69, 13, (byte)3 },
                    { 70, 13, (byte)4 },
                    { 71, 14, (byte)80 },
                    { 72, 14, (byte)75 },
                    { 73, 14, (byte)3 },
                    { 74, 14, (byte)4 },
                    { 75, 14, (byte)20 },
                    { 76, 14, (byte)0 },
                    { 77, 14, (byte)6 },
                    { 78, 14, (byte)0 },
                    { 79, 15, (byte)208 },
                    { 80, 15, (byte)207 },
                    { 81, 15, (byte)17 },
                    { 82, 15, (byte)224 },
                    { 83, 15, (byte)161 },
                    { 84, 15, (byte)177 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 85, 15, (byte)26 },
                    { 86, 15, (byte)225 },
                    { 87, 16, (byte)13 },
                    { 88, 16, (byte)68 },
                    { 89, 16, (byte)79 },
                    { 90, 16, (byte)67 },
                    { 91, 17, (byte)207 },
                    { 92, 17, (byte)17 },
                    { 93, 17, (byte)224 },
                    { 94, 17, (byte)161 },
                    { 95, 17, (byte)177 },
                    { 96, 17, (byte)26 },
                    { 97, 17, (byte)225 },
                    { 98, 17, (byte)0 },
                    { 99, 18, (byte)219 },
                    { 100, 18, (byte)165 },
                    { 101, 18, (byte)45 },
                    { 102, 18, (byte)0 },
                    { 103, 19, (byte)236 },
                    { 104, 19, (byte)165 },
                    { 105, 19, (byte)193 },
                    { 106, 19, (byte)0 },
                    { 107, 20, (byte)71 },
                    { 108, 20, (byte)73 },
                    { 109, 20, (byte)70 },
                    { 110, 20, (byte)56 },
                    { 111, 21, (byte)0 },
                    { 112, 21, (byte)0 },
                    { 113, 21, (byte)1 },
                    { 114, 21, (byte)0 },
                    { 115, 22, (byte)255 },
                    { 116, 22, (byte)216 },
                    { 117, 22, (byte)255 },
                    { 118, 22, (byte)224 },
                    { 119, 23, (byte)255 },
                    { 120, 23, (byte)216 },
                    { 121, 23, (byte)255 },
                    { 122, 23, (byte)225 },
                    { 123, 24, (byte)255 },
                    { 124, 24, (byte)216 },
                    { 125, 24, (byte)255 },
                    { 126, 24, (byte)232 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 127, 25, (byte)255 },
                    { 128, 25, (byte)216 },
                    { 129, 25, (byte)255 },
                    { 130, 25, (byte)224 },
                    { 131, 26, (byte)255 },
                    { 132, 26, (byte)216 },
                    { 133, 26, (byte)255 },
                    { 134, 26, (byte)226 },
                    { 135, 27, (byte)255 },
                    { 136, 27, (byte)216 },
                    { 137, 27, (byte)255 },
                    { 138, 27, (byte)227 },
                    { 139, 28, (byte)137 },
                    { 140, 28, (byte)80 },
                    { 141, 28, (byte)78 },
                    { 142, 28, (byte)71 },
                    { 143, 28, (byte)13 },
                    { 144, 28, (byte)10 },
                    { 145, 28, (byte)26 },
                    { 146, 28, (byte)10 },
                    { 147, 29, (byte)208 },
                    { 148, 29, (byte)207 },
                    { 149, 29, (byte)17 },
                    { 150, 29, (byte)224 },
                    { 151, 29, (byte)161 },
                    { 152, 29, (byte)177 },
                    { 153, 29, (byte)26 },
                    { 154, 29, (byte)225 },
                    { 155, 30, (byte)0 },
                    { 156, 30, (byte)110 },
                    { 157, 30, (byte)30 },
                    { 158, 30, (byte)240 },
                    { 159, 31, (byte)15 },
                    { 160, 31, (byte)0 },
                    { 161, 31, (byte)232 },
                    { 162, 31, (byte)3 },
                    { 163, 32, (byte)160 },
                    { 164, 32, (byte)70 },
                    { 165, 32, (byte)29 },
                    { 166, 32, (byte)240 },
                    { 167, 33, (byte)253 },
                    { 168, 33, (byte)255 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 169, 33, (byte)255 },
                    { 170, 33, (byte)255 },
                    { 171, 33, (byte)14 },
                    { 172, 33, (byte)0 },
                    { 173, 33, (byte)0 },
                    { 174, 33, (byte)0 },
                    { 175, 34, (byte)253 },
                    { 176, 34, (byte)255 },
                    { 177, 34, (byte)255 },
                    { 178, 34, (byte)255 },
                    { 179, 34, (byte)28 },
                    { 180, 34, (byte)0 },
                    { 181, 34, (byte)0 },
                    { 182, 34, (byte)0 },
                    { 183, 35, (byte)253 },
                    { 184, 35, (byte)255 },
                    { 185, 35, (byte)255 },
                    { 186, 35, (byte)255 },
                    { 187, 35, (byte)67 },
                    { 188, 35, (byte)0 },
                    { 189, 35, (byte)0 },
                    { 190, 35, (byte)0 },
                    { 191, 36, (byte)123 },
                    { 192, 36, (byte)92 },
                    { 193, 36, (byte)114 },
                    { 194, 36, (byte)116 },
                    { 195, 36, (byte)102 },
                    { 196, 36, (byte)49 },
                    { 197, 37, (byte)73 },
                    { 198, 37, (byte)32 },
                    { 199, 37, (byte)73 },
                    { 200, 38, (byte)73 },
                    { 201, 38, (byte)73 },
                    { 202, 38, (byte)42 },
                    { 203, 38, (byte)0 },
                    { 204, 39, (byte)77 },
                    { 205, 39, (byte)77 },
                    { 206, 39, (byte)0 },
                    { 207, 39, (byte)42 },
                    { 208, 40, (byte)77 },
                    { 209, 40, (byte)77 },
                    { 210, 40, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 211, 40, (byte)43 },
                    { 212, 41, (byte)73 },
                    { 213, 41, (byte)32 },
                    { 214, 41, (byte)73 },
                    { 215, 42, (byte)73 },
                    { 216, 42, (byte)73 },
                    { 217, 42, (byte)42 },
                    { 218, 42, (byte)0 },
                    { 219, 43, (byte)77 },
                    { 220, 43, (byte)77 },
                    { 221, 43, (byte)0 },
                    { 222, 43, (byte)42 },
                    { 223, 44, (byte)77 },
                    { 224, 44, (byte)77 },
                    { 225, 44, (byte)0 },
                    { 226, 44, (byte)43 },
                    { 227, 45, (byte)60 },
                    { 228, 45, (byte)63 },
                    { 229, 45, (byte)120 },
                    { 230, 45, (byte)109 },
                    { 231, 45, (byte)108 },
                    { 232, 45, (byte)32 },
                    { 233, 45, (byte)118 },
                    { 234, 45, (byte)101 },
                    { 235, 45, (byte)114 },
                    { 236, 45, (byte)115 },
                    { 237, 45, (byte)105 },
                    { 238, 45, (byte)111 },
                    { 239, 45, (byte)110 },
                    { 240, 45, (byte)61 },
                    { 241, 45, (byte)34 },
                    { 242, 45, (byte)49 },
                    { 243, 45, (byte)46 },
                    { 244, 45, (byte)48 },
                    { 245, 45, (byte)34 },
                    { 246, 45, (byte)63 },
                    { 247, 45, (byte)62 },
                    { 248, 46, (byte)80 },
                    { 249, 46, (byte)75 },
                    { 250, 46, (byte)3 },
                    { 251, 46, (byte)4 },
                    { 252, 47, (byte)80 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 253, 47, (byte)75 },
                    { 254, 47, (byte)76 },
                    { 255, 47, (byte)73 },
                    { 256, 47, (byte)84 },
                    { 257, 47, (byte)69 },
                    { 258, 48, (byte)80 },
                    { 259, 48, (byte)75 },
                    { 260, 48, (byte)83 },
                    { 261, 48, (byte)112 },
                    { 262, 48, (byte)88 },
                    { 263, 49, (byte)80 },
                    { 264, 49, (byte)75 },
                    { 265, 49, (byte)5 },
                    { 266, 49, (byte)6 },
                    { 267, 50, (byte)80 },
                    { 268, 50, (byte)75 },
                    { 269, 50, (byte)7 },
                    { 270, 50, (byte)8 },
                    { 271, 51, (byte)87 },
                    { 272, 51, (byte)105 },
                    { 273, 51, (byte)110 },
                    { 274, 51, (byte)90 },
                    { 275, 51, (byte)105 },
                    { 276, 51, (byte)112 },
                    { 277, 52, (byte)80 },
                    { 278, 52, (byte)75 },
                    { 279, 52, (byte)3 },
                    { 280, 52, (byte)4 },
                    { 281, 52, (byte)20 },
                    { 282, 52, (byte)0 },
                    { 283, 52, (byte)1 },
                    { 284, 52, (byte)0 },
                    { 285, 53, (byte)65 },
                    { 286, 53, (byte)114 },
                    { 287, 53, (byte)67 },
                    { 288, 53, (byte)1 },
                    { 289, 54, (byte)26 },
                    { 290, 54, (byte)2 },
                    { 291, 55, (byte)26 },
                    { 292, 55, (byte)3 },
                    { 293, 56, (byte)26 },
                    { 294, 56, (byte)4 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 295, 57, (byte)26 },
                    { 296, 57, (byte)8 },
                    { 297, 58, (byte)26 },
                    { 298, 58, (byte)9 },
                    { 299, 59, (byte)82 },
                    { 300, 59, (byte)73 },
                    { 301, 59, (byte)70 },
                    { 302, 59, (byte)70 },
                    { 303, 60, (byte)66 },
                    { 304, 60, (byte)76 },
                    { 305, 60, (byte)73 },
                    { 306, 60, (byte)50 },
                    { 307, 60, (byte)50 },
                    { 308, 60, (byte)51 },
                    { 309, 60, (byte)81 },
                    { 310, 61, (byte)66 },
                    { 311, 61, (byte)90 },
                    { 312, 61, (byte)104 },
                    { 313, 62, (byte)99 },
                    { 314, 62, (byte)117 },
                    { 315, 62, (byte)115 },
                    { 316, 62, (byte)104 },
                    { 317, 62, (byte)0 },
                    { 318, 62, (byte)0 },
                    { 319, 62, (byte)0 },
                    { 320, 62, (byte)2 },
                    { 321, 63, (byte)80 },
                    { 322, 63, (byte)75 },
                    { 323, 63, (byte)3 },
                    { 324, 63, (byte)4 },
                    { 325, 64, (byte)95 },
                    { 326, 64, (byte)39 },
                    { 327, 64, (byte)168 },
                    { 328, 64, (byte)137 },
                    { 329, 65, (byte)74 },
                    { 330, 65, (byte)65 },
                    { 331, 65, (byte)82 },
                    { 332, 65, (byte)67 },
                    { 333, 65, (byte)83 },
                    { 334, 65, (byte)0 },
                    { 335, 66, (byte)80 },
                    { 336, 66, (byte)75 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 337, 66, (byte)3 },
                    { 338, 66, (byte)4 },
                    { 339, 66, (byte)20 },
                    { 340, 66, (byte)0 },
                    { 341, 66, (byte)8 },
                    { 342, 66, (byte)0 },
                    { 343, 67, (byte)77 },
                    { 344, 67, (byte)84 },
                    { 345, 67, (byte)104 },
                    { 346, 67, (byte)100 },
                    { 347, 68, (byte)77 },
                    { 348, 68, (byte)84 },
                    { 349, 68, (byte)104 },
                    { 350, 68, (byte)100 },
                    { 351, 69, (byte)80 },
                    { 352, 69, (byte)75 },
                    { 353, 69, (byte)3 },
                    { 354, 69, (byte)4 },
                    { 355, 70, (byte)80 },
                    { 356, 70, (byte)75 },
                    { 357, 70, (byte)3 },
                    { 358, 70, (byte)4 },
                    { 359, 71, (byte)79 },
                    { 360, 71, (byte)103 },
                    { 361, 71, (byte)103 },
                    { 362, 71, (byte)83 },
                    { 363, 71, (byte)0 },
                    { 364, 71, (byte)2 },
                    { 365, 71, (byte)0 },
                    { 366, 71, (byte)0 },
                    { 367, 72, (byte)79 },
                    { 368, 72, (byte)103 },
                    { 369, 72, (byte)103 },
                    { 370, 72, (byte)83 },
                    { 371, 72, (byte)0 },
                    { 372, 72, (byte)2 },
                    { 373, 72, (byte)0 },
                    { 374, 72, (byte)0 },
                    { 375, 73, (byte)79 },
                    { 376, 73, (byte)103 },
                    { 377, 73, (byte)103 },
                    { 378, 73, (byte)83 }
                });

            migrationBuilder.InsertData(
                table: "ArquivoFormatoAssinaturaByte",
                columns: new[] { "Id", "ArquivoFormatoAssinaturaId", "Byte" },
                values: new object[,]
                {
                    { 379, 73, (byte)0 },
                    { 380, 73, (byte)2 },
                    { 381, 73, (byte)0 },
                    { 382, 73, (byte)0 },
                    { 383, 74, (byte)82 },
                    { 384, 74, (byte)97 },
                    { 385, 74, (byte)114 },
                    { 386, 74, (byte)33 },
                    { 387, 74, (byte)26 },
                    { 388, 74, (byte)7 },
                    { 389, 74, (byte)0 },
                    { 390, 75, (byte)67 },
                    { 391, 75, (byte)87 },
                    { 392, 75, (byte)83 },
                    { 393, 76, (byte)70 },
                    { 394, 76, (byte)87 },
                    { 395, 76, (byte)83 },
                    { 396, 77, (byte)117 },
                    { 397, 77, (byte)115 },
                    { 398, 77, (byte)116 },
                    { 399, 77, (byte)97 },
                    { 400, 77, (byte)114 },
                    { 401, 78, (byte)208 },
                    { 402, 78, (byte)207 },
                    { 403, 78, (byte)17 },
                    { 404, 78, (byte)224 },
                    { 405, 78, (byte)161 },
                    { 406, 78, (byte)177 },
                    { 407, 78, (byte)26 },
                    { 408, 78, (byte)225 },
                    { 409, 79, (byte)82 },
                    { 410, 79, (byte)73 },
                    { 411, 79, (byte)70 },
                    { 412, 79, (byte)70 }
                });

            migrationBuilder.InsertData(
                table: "PapelClaim",
                columns: new[] { "ClaimId", "PapelId" },
                values: new object[,]
                {
                    { 1001, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 1003, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 2001, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 2003, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 3001, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 3003, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 4001, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 4003, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" }
                });

            migrationBuilder.InsertData(
                table: "PapelClaim",
                columns: new[] { "ClaimId", "PapelId" },
                values: new object[,]
                {
                    { 5001, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 5003, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 6001, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 6003, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 6005, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 7001, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 7003, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { 1003, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 2003, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 3003, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 4003, "8a55ed94-eece-4e7f-ab46-43185864b7d1" },
                    { 5001, "8a55ed94-eece-4e7f-ab46-43185864b7d1" }
                });

            migrationBuilder.InsertData(
                table: "Pessoa",
                columns: new[] { "Id", "Ativo", "CriadoEm", "CriadoPorId", "Documento", "DocumentoTipoId", "Excluido", "Nome", "PaisId", "PessoaTipoId", "UltimaAlteracaoEm", "UltimaAlteracaoPorId" },
                values: new object[] { 1, true, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "11111111111111", 2, false, "KPMG - Administrativo", 1, 2, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "UserClaim",
                columns: new[] { "ClaimType", "ClaimValue", "UsuarioId" },
                values: new object[,]
                {
                    { "AlterarMeusAmbientes", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarMeusAnexos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarMeusDocumentosTipo", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarMeusProcessamentos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarMeusProjetos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarMeusUsuarios", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarMinhasEntidades", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosAmbientes", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosAnexos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosAnexosAssociados", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosDocumentosTipo", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosEntidades", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosProcessamentos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosProjetos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "AlterarTodosUsuarios", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarMeusAmbientes", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarMeusAnexos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarMeusDocumentosTipo", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarMeusProcessamentos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarMeusProjetos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarMeusUsuarios", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarMinhasEntidades", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarTodasAnexos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarTodasEntidades", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarTodasProcessamentos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarTodosAmbientes", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarTodosAnexosAssociados", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarTodosDocumentosTipo", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" },
                    { "VisualizarTodosProjetos", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" }
                });

            migrationBuilder.InsertData(
                table: "UserClaim",
                columns: new[] { "ClaimType", "ClaimValue", "UsuarioId" },
                values: new object[] { "VisualizarTodosUsuarios", "True", "f60753d7-c5a7-4496-a360-c1a301d87763" });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "AccessFailedCount", "CriadoEm", "CriadoPorId", "EmailBoasVindasEnviado", "Excluido", "Federado", "IdentityId", "IdentityIdGeradoLocalmente", "LockoutEnabled", "LockoutEndDateUtc", "Nome", "PapelId", "UltimaAlteracaoEm", "UltimaAlteracaoPorId", "UltimoAcessoEm", "UltimoAcessoIP", "UserName" },
                values: new object[] { "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", 0, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f60753d7-c5a7-4496-a360-c1a301d87763", false, false, false, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", false, false, null, "Administrador", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f60753d7-c5a7-4496-a360-c1a301d87763", null, null, "admin@kpmg.com" });

            migrationBuilder.InsertData(
                table: "PessoaJuridica",
                columns: new[] { "Id", "DadosCadastrais" },
                values: new object[] { 1, "" });

            migrationBuilder.InsertData(
                table: "UserClaim",
                columns: new[] { "ClaimType", "ClaimValue", "UsuarioId" },
                values: new object[,]
                {
                    { "AlterarMeusAmbientes", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarMeusAnexos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarMeusDocumentosTipo", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarMeusProcessamentos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarMeusProjetos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarMeusUsuarios", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarMinhasEntidades", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosAmbientes", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosAnexos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosAnexosAssociados", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosDocumentosTipo", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosEntidades", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosProcessamentos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosProjetos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "AlterarTodosUsuarios", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarMeusAmbientes", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarMeusAnexos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarMeusDocumentosTipo", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarMeusProcessamentos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarMeusProjetos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarMeusUsuarios", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarMinhasEntidades", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodasAnexos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodasEntidades", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodasProcessamentos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodosAmbientes", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodosAnexosAssociados", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodosDocumentosTipo", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodosProjetos", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" },
                    { "VisualizarTodosUsuarios", "True", "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" }
                });

            migrationBuilder.InsertData(
                table: "Ambiente",
                columns: new[] { "Id", "ClienteId", "CognitiveSearchIndexName", "CognitiveSearchSkillSetName", "CongnitiveSearchSize", "CriadoEm", "CriadoPorId", "Excluido", "LimiteTokenPorRequisicao", "NumeroEntidades", "QuantidadeMaximaUsuariosAtivos", "UltimaAlteracaoEm", "UltimaAlteracaoPorId" },
                values: new object[] { 1, 1, "omniai-index-vf", "skillset1695926195364", 3, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f60753d7-c5a7-4496-a360-c1a301d87763", false, 7160, 10, 10, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "f60753d7-c5a7-4496-a360-c1a301d87763" });

            migrationBuilder.InsertData(
                table: "UsuarioAmbiente",
                columns: new[] { "AmbienteId", "UsuarioId" },
                values: new object[] { 1, "6803f6e9-3f50-4b8d-b46b-fbea4827d2a4" });

            migrationBuilder.InsertData(
                table: "UsuarioAmbiente",
                columns: new[] { "AmbienteId", "UsuarioId" },
                values: new object[] { 1, "f60753d7-c5a7-4496-a360-c1a301d87763" });

            migrationBuilder.CreateIndex(
                name: "IX_Ambiente_ClienteId",
                table: "Ambiente",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambiente_CriadoPorId",
                table: "Ambiente",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Ambiente_UltimaAlteracaoPorId",
                table: "Ambiente",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_AnexoArquivoTipoId",
                table: "Anexo",
                column: "AnexoArquivoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_AnexoTipoId",
                table: "Anexo",
                column: "AnexoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_ArquivoFormatoId",
                table: "Anexo",
                column: "ArquivoFormatoId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_CriadoPorId",
                table: "Anexo",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Anexo_UltimaAlteracaoPorId",
                table: "Anexo",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_AnexoArquivoTipoArquivoFormato_ArquivoFormatoId",
                table: "AnexoArquivoTipoArquivoFormato",
                column: "ArquivoFormatoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnexoEmail_EmailId",
                table: "AnexoEmail",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_AnexoPagina_AnexoId",
                table: "AnexoPagina",
                column: "AnexoId");

            migrationBuilder.CreateIndex(
                name: "IX_AnexoPagina_AnexoPaiId",
                table: "AnexoPagina",
                column: "AnexoPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_AnexoPagina_PerguntaRespostaId",
                table: "AnexoPagina",
                column: "PerguntaRespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoFormatoAssinatura_ArquivoFormatoId",
                table: "ArquivoFormatoAssinatura",
                column: "ArquivoFormatoId");

            migrationBuilder.CreateIndex(
                name: "IX_ArquivoFormatoAssinaturaByte_ArquivoFormatoAssinaturaId",
                table: "ArquivoFormatoAssinaturaByte",
                column: "ArquivoFormatoAssinaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Claim_ClaimPaiId",
                table: "Claim",
                column: "ClaimPaiId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_PaisId",
                table: "Documento",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Documento_PessoaTipoId",
                table: "Documento",
                column: "PessoaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoTipo_AmbienteId",
                table: "DocumentoTipo",
                column: "AmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoTipo_CriadoPorId",
                table: "DocumentoTipo",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentoTipo_UltimaAlteracaoPorId",
                table: "DocumentoTipo",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Email_CorpoAnexoId",
                table: "Email",
                column: "CorpoAnexoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailDestinatario_EmailDestinatarioTipoId",
                table: "EmailDestinatario",
                column: "EmailDestinatarioTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDestinatario_EmailId",
                table: "EmailDestinatario",
                column: "EmailId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailDestinatarioUsuario_UsuarioId",
                table: "EmailDestinatarioUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidade_CriadoPorId",
                table: "Entidade",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidade_DocumentoTipoId",
                table: "Entidade",
                column: "DocumentoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entidade_UltimaAlteracaoPorId",
                table: "Entidade",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Interacao_CriadoPorId",
                table: "Interacao",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Interacao_InteracaoTipoId",
                table: "Interacao",
                column: "InteracaoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Interacao_UltimaAlteracaoPorId",
                table: "Interacao",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_InteracaoAmbiente_AmbienteId",
                table: "InteracaoAmbiente",
                column: "AmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_InteracaoUsuario_UsuarioId",
                table: "InteracaoUsuario",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PapelAcessivel_PapelAcessanteId",
                table: "PapelAcessivel",
                column: "PapelAcessanteId");

            migrationBuilder.CreateIndex(
                name: "IX_PapelClaim_ClaimId",
                table: "PapelClaim",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_PerguntaResposta_ProjetoId",
                table: "PerguntaResposta",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_CriadoPorId",
                table: "Pessoa",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_Documento",
                table: "Pessoa",
                column: "Documento");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_DocumentoTipoId",
                table: "Pessoa",
                column: "DocumentoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_PaisId",
                table: "Pessoa",
                column: "PaisId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_PessoaTipoId",
                table: "Pessoa",
                column: "PessoaTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_UltimaAlteracaoPorId",
                table: "Pessoa",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Processamento_CriadoPorId",
                table: "Processamento",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Processamento_ProcessamentoStatusId",
                table: "Processamento",
                column: "ProcessamentoStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Processamento_ProcessamentoTipoId",
                table: "Processamento",
                column: "ProcessamentoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_Processamento_UltimaAlteracaoPorId",
                table: "Processamento",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoAnexo_ProjetoAnexoId",
                table: "ProcessamentoAnexo",
                column: "ProjetoAnexoId",
                unique: true,
                filter: "[ProjetoAnexoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoEmail_EmailId",
                table: "ProcessamentoEmail",
                column: "EmailId",
                unique: true,
                filter: "[EmailId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoIndexer_LiberadoPorId",
                table: "ProcessamentoIndexer",
                column: "LiberadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoIndexer_ProjetoId",
                table: "ProcessamentoIndexer",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoLog_ProcessamentoId",
                table: "ProcessamentoLog",
                column: "ProcessamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoNer_ProjetoId",
                table: "ProcessamentoNer",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoPergunta_EntidadeId",
                table: "ProcessamentoPergunta",
                column: "EntidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoPergunta_PerguntaRespostaId",
                table: "ProcessamentoPergunta",
                column: "PerguntaRespostaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoPergunta_ProcessamentoNerId",
                table: "ProcessamentoPergunta",
                column: "ProcessamentoNerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessamentoPergunta_ProjetoAnexoId",
                table: "ProcessamentoPergunta",
                column: "ProjetoAnexoId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_AmbienteId",
                table: "Projeto",
                column: "AmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_CriadoPorId",
                table: "Projeto",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projeto_UltimaAlteracaoPorId",
                table: "Projeto",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAnexo_AnexoId",
                table: "ProjetoAnexo",
                column: "AnexoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAnexo_DocumentoTipoId",
                table: "ProjetoAnexo",
                column: "DocumentoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAnexo_ProcessamentoIndexerId",
                table: "ProjetoAnexo",
                column: "ProcessamentoIndexerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjetoAnexo_ProjetoId",
                table: "ProjetoAnexo",
                column: "ProjetoId");

            migrationBuilder.CreateIndex(
                name: "IX_RelacaoClaimGrupo_ClaimGrupoId",
                table: "RelacaoClaimGrupo",
                column: "ClaimGrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_RelacaoClaimGrupo_ClaimId",
                table: "RelacaoClaimGrupo",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_TemaConfiguracao_UsuarioId",
                table: "TemaConfiguracao",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_CriadoPorId",
                table: "Usuario",
                column: "CriadoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_PapelId",
                table: "Usuario",
                column: "PapelId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UltimaAlteracaoPorId",
                table: "Usuario",
                column: "UltimaAlteracaoPorId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_UserName",
                table: "Usuario",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioAmbiente_AmbienteId",
                table: "UsuarioAmbiente",
                column: "AmbienteId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogin_UserId",
                table: "UsuarioLogin",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnexoArquivoTipoArquivoFormato");

            migrationBuilder.DropTable(
                name: "AnexoEmail");

            migrationBuilder.DropTable(
                name: "AnexoPagina");

            migrationBuilder.DropTable(
                name: "ArquivoFormatoAssinaturaByte");

            migrationBuilder.DropTable(
                name: "EmailDestinatarioUsuario");

            migrationBuilder.DropTable(
                name: "InteracaoAmbiente");

            migrationBuilder.DropTable(
                name: "InteracaoUsuario");

            migrationBuilder.DropTable(
                name: "PapelAcessivel");

            migrationBuilder.DropTable(
                name: "PapelClaim");

            migrationBuilder.DropTable(
                name: "ProcessamentoAnexo");

            migrationBuilder.DropTable(
                name: "ProcessamentoEmail");

            migrationBuilder.DropTable(
                name: "ProcessamentoLog");

            migrationBuilder.DropTable(
                name: "ProcessamentoPergunta");

            migrationBuilder.DropTable(
                name: "RelacaoClaimGrupo");

            migrationBuilder.DropTable(
                name: "TemaConfiguracao");

            migrationBuilder.DropTable(
                name: "UserClaim");

            migrationBuilder.DropTable(
                name: "UsuarioAmbiente");

            migrationBuilder.DropTable(
                name: "UsuarioLogin");

            migrationBuilder.DropTable(
                name: "ArquivoFormatoAssinatura");

            migrationBuilder.DropTable(
                name: "EmailDestinatario");

            migrationBuilder.DropTable(
                name: "Interacao");

            migrationBuilder.DropTable(
                name: "Entidade");

            migrationBuilder.DropTable(
                name: "PerguntaResposta");

            migrationBuilder.DropTable(
                name: "ProcessamentoNer");

            migrationBuilder.DropTable(
                name: "ProjetoAnexo");

            migrationBuilder.DropTable(
                name: "Claim");

            migrationBuilder.DropTable(
                name: "ClaimGrupo");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "EmailDestinatarioTipo");

            migrationBuilder.DropTable(
                name: "InteracaoTipo");

            migrationBuilder.DropTable(
                name: "DocumentoTipo");

            migrationBuilder.DropTable(
                name: "ProcessamentoIndexer");

            migrationBuilder.DropTable(
                name: "Anexo");

            migrationBuilder.DropTable(
                name: "Processamento");

            migrationBuilder.DropTable(
                name: "Projeto");

            migrationBuilder.DropTable(
                name: "AnexoArquivoTipo");

            migrationBuilder.DropTable(
                name: "AnexoTipo");

            migrationBuilder.DropTable(
                name: "ArquivoFormato");

            migrationBuilder.DropTable(
                name: "ProcessamentoStatus");

            migrationBuilder.DropTable(
                name: "ProcessamentoTipo");

            migrationBuilder.DropTable(
                name: "Ambiente");

            migrationBuilder.DropTable(
                name: "PessoaJuridica");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "Documento");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Pais");

            migrationBuilder.DropTable(
                name: "PessoaTipo");

            migrationBuilder.DropTable(
                name: "Papel");
        }
    }
}
