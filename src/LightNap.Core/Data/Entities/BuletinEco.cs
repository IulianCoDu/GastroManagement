namespace LightNap.Core.Data.Entities
{
    /// <summary>
    /// Represents an ultrasound/echography medical report
    /// </summary>
    public class BuletinEco
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Report number
        /// </summary>
        public int Nr { get; set; }

        /// <summary>
        /// Patient's last name
        /// </summary>
        public string Nume { get; set; } = string.Empty;

        /// <summary>
        /// Patient's first name
        /// </summary>
        public string Prenume { get; set; } = string.Empty;

        /// <summary>
        /// Patient's age
        /// </summary>
        public int Virsta { get; set; }

        /// <summary>
        /// Patient's national identification number (CNP - Cod Numeric Personal)
        /// </summary>
        public string Cnp { get; set; } = string.Empty;

        /// <summary>
        /// ID card series
        /// </summary>
        public string? SeriaCs { get; set; }

        /// <summary>
        /// Patient's address
        /// </summary>
        public string? Domiciliu { get; set; }

        /// <summary>
        /// Patient's phone number
        /// </summary>
        public string? Telefon { get; set; }

        /// <summary>
        /// Medical diagnosis
        /// </summary>
        public string? Diagnostic { get; set; }

        /// <summary>
        /// Liver examination findings
        /// </summary>
        public string? Ficat { get; set; }

        /// <summary>
        /// Gallbladder examination findings
        /// </summary>
        public string? Colecist { get; set; }

        /// <summary>
        /// Portal vein measurement
        /// </summary>
        public int? Vp { get; set; }

        /// <summary>
        /// Splenic vein measurement
        /// </summary>
        public short? Vs { get; set; }

        /// <summary>
        /// Common bile duct measurement
        /// </summary>
        public int? Cbp { get; set; }

        /// <summary>
        /// Pancreas examination findings
        /// </summary>
        public string? Pancreas { get; set; }

        /// <summary>
        /// Spleen examination findings
        /// </summary>
        public string? Splina { get; set; }

        /// <summary>
        /// Right kidney examination findings
        /// </summary>
        public string? Rd { get; set; }

        /// <summary>
        /// Left kidney examination findings
        /// </summary>
        public string? Rs { get; set; }

        /// <summary>
        /// Urinary bladder examination findings
        /// </summary>
        public string? Vu { get; set; }

        /// <summary>
        /// Prostate examination findings
        /// </summary>
        public string? Prostata { get; set; }

        /// <summary>
        /// Internal genital organs examination findings
        /// </summary>
        public string? Ogi { get; set; }

        /// <summary>
        /// Observations/Notes
        /// </summary>
        public string? Obs { get; set; }

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
        public string Medic { get; set; } = string.Empty;

        /// <summary>
        /// Optional reference to the performing doctor (Medici table)
        /// </summary>
        public int? MedicId { get; set; }

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
        /// Contrast-enhanced ultrasound findings
        /// </summary>
        public string? Ceus { get; set; }

    }
}
