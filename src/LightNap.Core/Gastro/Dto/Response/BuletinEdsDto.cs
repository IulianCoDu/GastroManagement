using System;

namespace LightNap.Core.Gastro.Dto.Response
{
    public sealed class BuletinEdsDto
    {
        public int Id { get; set; }
        public int? Nr { get; set; }
        public string? Nume { get; set; }
        public string? Prenume { get; set; }
        public byte? Virsta { get; set; }
        public string? Cnp { get; set; }
        public string? Domiciliu { get; set; }
        public string? Diagnostic { get; set; }
        public string Sistem { get; set; } = string.Empty;
        public string? SedareT { get; set; }
        public string? Medicatie { get; set; }
        public string? Esofag { get; set; }
        public string? Jonctiune { get; set; }
        public string? Stomac { get; set; }
        public string? Pilor { get; set; }
        public string? Bulb { get; set; }
        public string? Duoden { get; set; }
        public string? BiopsiiL { get; set; }
        public short? BiopsiiN { get; set; }
        public int? Nrap { get; set; }
        public string? BiopsiiR { get; set; }
        public string? Tratament { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Ora { get; set; }
        public int? MedicId { get; set; }
        public string? Medic { get; set; }
    }
}
