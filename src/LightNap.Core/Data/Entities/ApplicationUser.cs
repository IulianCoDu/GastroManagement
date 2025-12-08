using Microsoft.AspNetCore.Identity;

namespace LightNap.Core.Data.Entities
{
    /// <summary>
    /// Represents an application user with additional properties.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ApplicationUser"/> class.
    /// </remarks>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The date when the user was created.
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The date when the user was last modified.
        /// </summary>
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// The doctor's name (for medical staff users).
        /// Stored directly on user for quick access without joins.
        /// </summary>
        public string? MedicName { get; set; }

        /// <summary>
        /// The associated doctor profile (optional, for medical staff users).
        /// Created during registration when user enters their doctor name.
        /// </summary>
        public Medic? Medic { get; set; }

        /// <summary>
        /// The notifications associated with the user.
        /// </summary>
        public ICollection<Notification>? Notifications { get; set; }

        /// <summary>
        /// The refresh tokens associated with the user.
        /// </summary>
        public ICollection<RefreshToken>? RefreshTokens { get; set; }

        /// <summary>
        /// The settings associated with the user.
        /// </summary>
        public ICollection<UserSetting>? UserSettings { get; set; }
    }
}
