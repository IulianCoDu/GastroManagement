using System;

namespace LightNap.Core.Gastro.Dto.Request
{
    public sealed class CreateBuletinEdiDto
    {
        public string? Nume { get; set; }
        public string? Prenume { get; set; }
        public byte? Virsta { get; set; }
        public string? Cnp { get; set; }
        public string? Domiciliu { get; set; }
        public string? Diagnostic { get; set; }
        public string Sistem { get; set; } = string.Empty;
        public string? SedareT { get; set; }
        public string? Medicatie { get; set; }
        public string? Ileon { get; set; }
        public string? Cec { get; set; }
        public string? Ascendent { get; set; }
        public string? Transvers { get; set; }
        public string? Descendent { get; set; }
        public string? Sigmoid { get; set; }
        public string? Rect { get; set; }
        public string? Jar { get; set; }
        public string? BiopsiiL { get; set; }
        public short? BiopsiiN { get; set; }
        public int? Nrap { get; set; }
        public string? BiopsiiR { get; set; }
        public string? Tratament { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? Ora { get; set; }
        public int? MedicId { get; set; }
        public string? Medic { get; set; }
        public string? BiopsiiL1 { get; set; }
        public short? BiopsiiN1 { get; set; }
        public int? Nrap1 { get; set; }
        public string? BiopsiiR1 { get; set; }
        public DateTime? Data1 { get; set; }
        public DateTime? Ora1 { get; set; }
        public string? SedareT1 { get; set; }
        public int? DozaS { get; set; }
        public string? Medicatie1 { get; set; }
        public int? DozaM { get; set; }
        public string? Consumabile { get; set; }
        public string? Materiale { get; set; }
    }
}
