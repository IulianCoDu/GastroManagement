using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LightNap.DataProviders.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddGastroTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medici",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medici", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sistem",
                columns: table => new
                {
                    Numarsistem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sistem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sistem", x => x.Numarsistem);
                });

            migrationBuilder.CreateTable(
                name: "Buletine_ECO",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nr = table.Column<int>(type: "int", nullable: false),
                    nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    virsta = table.Column<int>(type: "int", nullable: false),
                    CNP = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    seriaCS = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    domiciliu = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true, defaultValue: "CRAIOVA, DOLJ"),
                    telefon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ficat = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "DIAMETRU ANTEROPOSTERIOR LOB STING 0 cm, PRERENAL LOB DREPT 0 cm, ECOSTRUCTURA OMOGENA, CONTUR REGULAT, FARA PROCESE LOCALIZATE"),
                    colecist = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "PERETI NORMALI, FARA CALCULI"),
                    VP = table.Column<int>(type: "int", nullable: true),
                    VS = table.Column<short>(type: "smallint", nullable: true),
                    CBP = table.Column<int>(type: "int", nullable: true),
                    pancreas = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "OMOGEN, DIMENSIUNI NORMALE"),
                    splina = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "0 cm AX LUNG"),
                    rd = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "0 cm AX LUNG, IP 0 cm, FARA CALCULI, FARA DILATATII"),
                    rs = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "0 cm AX LUNG, IP 0 cm, FARA CALCULI, FARA DILATATII"),
                    vu = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "GOLITA"),
                    prostata = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMALA"),
                    ogi = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMALE"),
                    obs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    ora = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    medic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Fig1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Fig2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Fig3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Film1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CEUS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSMA_TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    MedicId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buletine_ECO", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Buletine_ECO_Medici_MedicId",
                        column: x => x.MedicId,
                        principalTable: "Medici",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Buletine_EDI",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nr = table.Column<int>(type: "int", nullable: true),
                    nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    virsta = table.Column<byte>(type: "tinyint", nullable: true),
                    CNP = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    domiciliu = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true, defaultValue: "CRAIOVA, DOLJ"),
                    diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "OLYMPUS EXERA"),
                    sedare_t = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    medicatie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Ileon = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Cec = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Ascendent = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Transvers = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Descendent = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Sigmoid = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Rect = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    JAR = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    biopsii_l = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    biopsii_n = table.Column<short>(type: "smallint", nullable: true),
                    nrap = table.Column<int>(type: "int", nullable: true),
                    biopsii_r = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tratament = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    ora = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    medic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    biopsii_l1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    biopsii_n1 = table.Column<short>(type: "smallint", nullable: true),
                    nrap1 = table.Column<int>(type: "int", nullable: true),
                    biopsii_r1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data1 = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    ora1 = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    sedare_t1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    doza_s = table.Column<int>(type: "int", nullable: true),
                    medicatie1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    doza_m = table.Column<int>(type: "int", nullable: true),
                    Fig1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Fig2 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Fig3 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Film1 = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Consumabile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Materiale = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SSMA_TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    MedicId = table.Column<int>(type: "int", nullable: true),
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
                });

            migrationBuilder.CreateTable(
                name: "Buletine_EDS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nr = table.Column<int>(type: "int", nullable: true),
                    nume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    prenume = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    virsta = table.Column<byte>(type: "tinyint", nullable: true),
                    CNP = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    domiciliu = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true, defaultValue: "CRAIOVA, DOLJ"),
                    diagnostic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sistem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "OLYMPUS EXERA"),
                    sedare_t = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    medicatie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Esofag = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Jonctiune = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Stomac = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "NORMAL"),
                    Pilor = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    Bulb = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    Duoden = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true, defaultValue: "NORMAL"),
                    biopsii_l = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    biopsii_n = table.Column<short>(type: "smallint", nullable: true),
                    nrap = table.Column<int>(type: "int", nullable: true),
                    biopsii_r = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tratament = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    ora = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    medic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SSMA_TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
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
                });

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Buletine_ECO");

            migrationBuilder.DropTable(
                name: "Buletine_EDI");

            migrationBuilder.DropTable(
                name: "Buletine_EDS");

            migrationBuilder.DropTable(
                name: "Medici");

            migrationBuilder.DropTable(
                name: "Sistem");
        }
    }
}
