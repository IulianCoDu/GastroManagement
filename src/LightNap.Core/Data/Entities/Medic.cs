namespace LightNap.Core.Data.Entities
{
    /// <summary>
    /// Represents a doctor/physician in the gastroenterology system.
    /// Created automatically when a medical staff user registers.
    /// </summary>
    public class Medic
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Doctor's name (entered during user registration)
        /// </summary>
        public string MedicName { get; set; } = string.Empty;

        /// <summary>
        /// The user ID associated with this doctor (foreign key)
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        // Navigation properties

        /// <summary>
        /// The user account associated with this doctor (one-to-one, required)
        /// </summary>
        public ApplicationUser User { get; set; } = null!;

        /// <summary>
        /// Ultrasound reports created by this doctor
        /// </summary>
        public ICollection<BuletinEco> BuletineEco { get; set; } = [];

        /// <summary>
        /// Upper GI endoscopy reports created by this doctor
        /// </summary>
        public ICollection<BuletinEds> BuletineEds { get; set; } = [];

        /// <summary>
        /// Colonoscopy reports created by this doctor
        /// </summary>
        public ICollection<BuletinEdi> BuletineEdi { get; set; } = [];
    }
}
