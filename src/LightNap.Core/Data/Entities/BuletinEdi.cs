namespace LightNap.Core.Data.Entities
{
    /// <summary>
    /// Represents a colonoscopy (EDI - Lower GI Endoscopy) medical report
    /// </summary>
    public class BuletinEdi
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Report number
        /// </summary>
        public int? Nr { get; set; }

        /// <summary>
        /// Patient's last name
        /// </summary>
        public string? Nume { get; set; }

        /// <summary>
        /// Patient's first name
        /// </summary>
        public string? Prenume { get; set; }

        /// <summary>
        /// Patient's age
        /// </summary>
        public byte? Virsta { get; set; }

        /// <summary>
        /// Patient's national identification number (CNP - Cod Numeric Personal)
        /// </summary>
        public string? Cnp { get; set; }

        /// <summary>
        /// Patient's address
        /// </summary>
        public string? Domiciliu { get; set; }

        /// <summary>
        /// Medical diagnosis
        /// </summary>
        public string? Diagnostic { get; set; }

        /// <summary>
        /// Equipment/System used
        /// </summary>
        public string Sistem { get; set; } = string.Empty;

        /// <summary>
        /// Type of sedation used
        /// </summary>
        public string? SedareT { get; set; }

        /// <summary>
        /// Medication administered
        /// </summary>
        public string? Medicatie { get; set; }

        /// <summary>
        /// Ileum examination findings
        /// </summary>
        public string? Ileon { get; set; }

        /// <summary>
        /// Cecum examination findings
        /// </summary>
        public string? Cec { get; set; }

        /// <summary>
        /// Ascending colon examination findings
        /// </summary>
        public string? Ascendent { get; set; }

        /// <summary>
        /// Transverse colon examination findings
        /// </summary>
        public string? Transvers { get; set; }

        /// <summary>
        /// Descending colon examination findings
        /// </summary>
        public string? Descendent { get; set; }

        /// <summary>
        /// Sigmoid colon examination findings
        /// </summary>
        public string? Sigmoid { get; set; }

        /// <summary>
        /// Rectum examination findings
        /// </summary>
        public string? Rect { get; set; }

        /// <summary>
        /// Anorectal junction examination findings
        /// </summary>
        public string? Jar { get; set; }

        /// <summary>
        /// Primary biopsy location
        /// </summary>
        public string? BiopsiiL { get; set; }

        /// <summary>
        /// Number of primary biopsies taken
        /// </summary>
        public short? BiopsiiN { get; set; }

        /// <summary>
        /// Primary anatomical pathology report number
        /// </summary>
        public int? Nrap { get; set; }

        /// <summary>
        /// Primary biopsy results
        /// </summary>
        public string? BiopsiiR { get; set; }

        /// <summary>
        /// Treatment performed
        /// </summary>
        public string? Tratament { get; set; }

        /// <summary>
        /// Examination date
        /// </summary>
        public DateTime? Data { get; set; }

        /// <summary>
        /// Examination time
        /// </summary>
        public DateTime? Ora { get; set; }

        /// <summary>
        /// Performing doctor's name
        /// </summary>
        public string? Medic { get; set; }

        /// <summary>
        /// Optional reference to the performing doctor (Medici table)
        /// </summary>
        public int? MedicId { get; set; }

        /// <summary>
        /// Secondary biopsy location
        /// </summary>
        public string? BiopsiiL1 { get; set; }

        /// <summary>
        /// Number of secondary biopsies taken
        /// </summary>
        public short? BiopsiiN1 { get; set; }

        /// <summary>
        /// Secondary anatomical pathology report number
        /// </summary>
        public int? Nrap1 { get; set; }

        /// <summary>
        /// Secondary biopsy results
        /// </summary>
        public string? BiopsiiR1 { get; set; }

        /// <summary>
        /// Follow-up examination date
        /// </summary>
        public DateTime? Data1 { get; set; }

        /// <summary>
        /// Follow-up examination time
        /// </summary>
        public DateTime? Ora1 { get; set; }

        /// <summary>
        /// Secondary sedation type
        /// </summary>
        public string? SedareT1 { get; set; }

        /// <summary>
        /// Sedation dosage
        /// </summary>
        public int? DozaS { get; set; }

        /// <summary>
        /// Secondary medication
        /// </summary>
        public string? Medicatie1 { get; set; }

        /// <summary>
        /// Medication dosage
        /// </summary>
        public int? DozaM { get; set; }

        /// <summary>
        /// First image attachment
        /// </summary>
        public byte[]? Fig1 { get; set; }

        /// <summary>
        /// Second image attachment
        /// </summary>
        public byte[]? Fig2 { get; set; }

        /// <summary>
        /// Third image attachment
        /// </summary>
        public byte[]? Fig3 { get; set; }

        /// <summary>
        /// Video attachment
        /// </summary>
        public byte[]? Film1 { get; set; }

        /// <summary>
        /// Consumables used during procedure
        /// </summary>
        public string? Consumabile { get; set; }

        /// <summary>
        /// Materials used during procedure
        /// </summary>
        public string? Materiale { get; set; }

    }
}
