using System;

namespace LightNap.Core.Gastro.Dto.Response
{
    public sealed class BuletinEcoDto
    {
        public int Id { get; set; }
        public int Nr { get; set; }
        public string Nume { get; set; } = string.Empty;
        public string Prenume { get; set; } = string.Empty;
        public int Virsta { get; set; }
        public string Cnp { get; set; } = string.Empty;
        public string? SeriaCs { get; set; }
        public string? Domiciliu { get; set; }
        public string? Telefon { get; set; }
        public string? Diagnostic { get; set; }
        public string? Ficat { get; set; }
        public string? Colecist { get; set; }
        public int? Vp { get; set; }
        public short? Vs { get; set; }
        public int? Cbp { get; set; }
        public string? Pancreas { get; set; }
        public string? Splina { get; set; }
        public string? Rd { get; set; }
        public string? Rs { get; set; }
        public string? Vu { get; set; }
        public string? Prostata { get; set; }
        public string? Ogi { get; set; }
        public string? Obs { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Ora { get; set; }
        public string Medic { get; set; } = string.Empty;
        public string? Ceus { get; set; }
        public bool HasFig1 { get; set; }
        public bool HasFig2 { get; set; }
        public bool HasFig3 { get; set; }
        public bool HasFilm1 { get; set; }
    }
}
