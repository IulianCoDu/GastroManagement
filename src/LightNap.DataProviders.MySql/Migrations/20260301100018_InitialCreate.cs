using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace LightNap.DataProviders.MySql.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    DisplayName = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MedicName = table.Column<string>(type: "longtext", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sistem",
                columns: table => new
                {
                    Numarsistem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Sistem = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistem", x => x.Numarsistem);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Medici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Medic = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medici", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medici_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Token = table.Column<string>(type: "longtext", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsRevoked = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsPersistent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Details = table.Column<string>(type: "longtext", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StaticContents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Key = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusChangedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    StatusChangedByUserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedByUserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    EditorRoles = table.Column<string>(type: "longtext", nullable: true),
                    ReadAccess = table.Column<int>(type: "int", nullable: false),
                    ReaderRoles = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaticContents_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticContents_AspNetUsers_LastModifiedByUserId",
                        column: x => x.LastModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticContents_AspNetUsers_StatusChangedByUserId",
                        column: x => x.StatusChangedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserSettings",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    Key = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSettings", x => new { x.UserId, x.Key });
                    table.ForeignKey(
                        name: "FK_UserSettings_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Buletine_ECO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nr = table.Column<int>(type: "int", nullable: false),
                    nume = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    prenume = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    virsta = table.Column<int>(type: "int", nullable: false),
                    CNP = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: false),
                    seriaCS = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    domiciliu = table.Column<string>(type: "varchar(125)", maxLength: 125, nullable: true, defaultValue: "CRAIOVA, DOLJ"),
                    telefon = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    diagnostic = table.Column<string>(type: "longtext", nullable: true),
                    ficat = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('DIAMETRU ANTEROPOSTERIOR LOB STING 0 cm, PRERENAL LOB DREPT 0 cm, ECOSTRUCTURA OMOGENA, CONTUR REGULAT, FARA PROCESE LOCALIZATE')"),
                    colecist = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('PERETI NORMALI, FARA CALCULI')"),
                    VP = table.Column<int>(type: "int", nullable: true),
                    VS = table.Column<short>(type: "smallint", nullable: true),
                    CBP = table.Column<int>(type: "int", nullable: true),
                    pancreas = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('OMOGEN, DIMENSIUNI NORMALE')"),
                    splina = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('0 cm AX LUNG')"),
                    rd = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('0 cm AX LUNG, IP 0 cm, FARA CALCULI, FARA DILATATII')"),
                    rs = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('0 cm AX LUNG, IP 0 cm, FARA CALCULI, FARA DILATATII')"),
                    vu = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('GOLITA')"),
                    prostata = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMALA')"),
                    ogi = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMALE')"),
                    obs = table.Column<string>(type: "longtext", nullable: true),
                    data = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ora = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    medic = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    MedicId = table.Column<int>(type: "int", nullable: true),
                    Fig1 = table.Column<byte[]>(type: "longblob", nullable: true),
                    Fig2 = table.Column<byte[]>(type: "longblob", nullable: true),
                    Fig3 = table.Column<byte[]>(type: "longblob", nullable: true),
                    Film1 = table.Column<byte[]>(type: "longblob", nullable: true),
                    CEUS = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buletine_ECO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buletine_ECO_Medici_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medici",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Buletine_EDI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nr = table.Column<int>(type: "int", nullable: true),
                    nume = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    prenume = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    virsta = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    CNP = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    domiciliu = table.Column<string>(type: "varchar(125)", maxLength: 125, nullable: true, defaultValue: "CRAIOVA, DOLJ"),
                    diagnostic = table.Column<string>(type: "longtext", nullable: true),
                    Sistem = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "OLYMPUS EXERA"),
                    sedare_t = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    medicatie = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Ileon = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Cec = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Ascendent = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Transvers = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Descendent = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Sigmoid = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Rect = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    JAR = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    biopsii_l = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    biopsii_n = table.Column<short>(type: "smallint", nullable: true),
                    nrap = table.Column<int>(type: "int", nullable: true),
                    biopsii_r = table.Column<string>(type: "longtext", nullable: true),
                    Tratament = table.Column<string>(type: "longtext", nullable: true),
                    data = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ora = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    medic = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MedicId = table.Column<int>(type: "int", nullable: true),
                    biopsii_l1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    biopsii_n1 = table.Column<short>(type: "smallint", nullable: true),
                    nrap1 = table.Column<int>(type: "int", nullable: true),
                    biopsii_r1 = table.Column<string>(type: "longtext", nullable: true),
                    data1 = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ora1 = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    sedare_t1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    doza_s = table.Column<int>(type: "int", nullable: true),
                    medicatie1 = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    doza_m = table.Column<int>(type: "int", nullable: true),
                    Fig1 = table.Column<byte[]>(type: "longblob", nullable: true),
                    Fig2 = table.Column<byte[]>(type: "longblob", nullable: true),
                    Fig3 = table.Column<byte[]>(type: "longblob", nullable: true),
                    Film1 = table.Column<byte[]>(type: "longblob", nullable: true),
                    Consumabile = table.Column<string>(type: "longtext", nullable: true),
                    Materiale = table.Column<string>(type: "longtext", nullable: true),
                    SistemNumarsistem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buletine_EDI", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buletine_EDI_Medici_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medici",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buletine_EDI_Sistem_SistemNumarsistem",
                        column: x => x.SistemNumarsistem,
                        principalTable: "Sistem",
                        principalColumn: "Numarsistem");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Buletine_EDS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    nr = table.Column<int>(type: "int", nullable: true),
                    nume = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    prenume = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    virsta = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    CNP = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    domiciliu = table.Column<string>(type: "varchar(125)", maxLength: 125, nullable: true, defaultValue: "CRAIOVA, DOLJ"),
                    diagnostic = table.Column<string>(type: "longtext", nullable: true),
                    Sistem = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, defaultValue: "OLYMPUS EXERA"),
                    sedare_t = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    medicatie = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Esofag = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Jonctiune = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Stomac = table.Column<string>(type: "longtext", nullable: true, defaultValueSql: "('NORMAL')"),
                    Pilor = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    Bulb = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    Duoden = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    biopsii_l = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    biopsii_n = table.Column<short>(type: "smallint", nullable: true),
                    nrap = table.Column<int>(type: "int", nullable: true),
                    biopsii_r = table.Column<string>(type: "longtext", nullable: true),
                    Tratament = table.Column<string>(type: "longtext", nullable: true),
                    data = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ora = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    medic = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MedicId = table.Column<int>(type: "int", nullable: true),
                    SistemNumarsistem = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buletine_EDS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buletine_EDS_Medici_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medici",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Buletine_EDS_Sistem_SistemNumarsistem",
                        column: x => x.SistemNumarsistem,
                        principalTable: "Sistem",
                        principalColumn: "Numarsistem");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StaticContentLanguages",
                columns: table => new
                {
                    StaticContentId = table.Column<int>(type: "int", nullable: false),
                    LanguageCode = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false),
                    Content = table.Column<string>(type: "longtext", nullable: false),
                    Format = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedByUserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedUserId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaticContentLanguages", x => new { x.StaticContentId, x.LanguageCode });
                    table.ForeignKey(
                        name: "FK_StaticContentLanguages_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticContentLanguages_AspNetUsers_LastModifiedUserId",
                        column: x => x.LastModifiedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaticContentLanguages_StaticContents_StaticContentId",
                        column: x => x.StaticContentId,
                        principalTable: "StaticContents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Buletine_ECO_MedicId",
                table: "Buletine_ECO",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Buletine_EDI_MedicId",
                table: "Buletine_EDI",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Buletine_EDI_SistemNumarsistem",
                table: "Buletine_EDI",
                column: "SistemNumarsistem");

            migrationBuilder.CreateIndex(
                name: "IX_Buletine_EDS_MedicId",
                table: "Buletine_EDS",
                column: "MedicId");

            migrationBuilder.CreateIndex(
                name: "IX_Buletine_EDS_SistemNumarsistem",
                table: "Buletine_EDS",
                column: "SistemNumarsistem");

            migrationBuilder.CreateIndex(
                name: "IX_Medici_UserId",
                table: "Medici",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContentLanguages_CreatedByUserId",
                table: "StaticContentLanguages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContentLanguages_LanguageCode",
                table: "StaticContentLanguages",
                column: "LanguageCode");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContentLanguages_LastModifiedUserId",
                table: "StaticContentLanguages",
                column: "LastModifiedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContents_CreatedByUserId",
                table: "StaticContents",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContents_Key",
                table: "StaticContents",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaticContents_LastModifiedByUserId",
                table: "StaticContents",
                column: "LastModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaticContents_StatusChangedByUserId",
                table: "StaticContents",
                column: "StatusChangedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Buletine_ECO");

            migrationBuilder.DropTable(
                name: "Buletine_EDI");

            migrationBuilder.DropTable(
                name: "Buletine_EDS");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "StaticContentLanguages");

            migrationBuilder.DropTable(
                name: "UserSettings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Medici");

            migrationBuilder.DropTable(
                name: "Sistem");

            migrationBuilder.DropTable(
                name: "StaticContents");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
