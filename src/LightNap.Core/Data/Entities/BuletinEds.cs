namespace LightNap.Core.Data.Entities
{
    /// <summary>
    /// Represents an upper GI endoscopy (EDS - Esophagogastroduodenoscopy) medical report
    /// </summary>
    public class BuletinEds
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
        /// Esophagus examination findings
        /// </summary>
        public string? Esofag { get; set; }

        /// <summary>
        /// Gastroesophageal junction examination findings
        /// </summary>
        public string? Jonctiune { get; set; }

        /// <summary>
        /// Stomach examination findings
        /// </summary>
        public string? Stomac { get; set; }

        /// <summary>
        /// Pylorus examination findings
        /// </summary>
        public string? Pilor { get; set; }

        /// <summary>
        /// Duodenal bulb examination findings
        /// </summary>
        public string? Bulb { get; set; }

        /// <summary>
        /// Duodenum examination findings
        /// </summary>
        public string? Duoden { get; set; }

        /// <summary>
        /// Biopsy location
        /// </summary>
        public string? BiopsiiL { get; set; }

        /// <summary>
        /// Number of biopsies taken
        /// </summary>
        public short? BiopsiiN { get; set; }

        /// <summary>
        /// Anatomical pathology report number
        /// </summary>
        public int? Nrap { get; set; }

        /// <summary>
        /// Biopsy results
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
        /// Row version for concurrency
        /// </summary>
        public byte[] SsmaTimeStamp { get; set; } = [];
    }
}
