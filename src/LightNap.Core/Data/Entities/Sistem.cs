namespace LightNap.Core.Data.Entities
{
    /// <summary>
    /// Represents medical equipment/systems used for procedures
    /// </summary>
    public class Sistem
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Numarsistem { get; set; }

        /// <summary>
        /// System/Equipment name
        /// </summary>
        public string? Sistem1 { get; set; }

        // Navigation properties
        public ICollection<BuletinEds> BuletineEds { get; set; } = [];
        public ICollection<BuletinEdi> BuletineEdi { get; set; } = [];
    }
}
