using LightNap.Core.Configuration;
using System.ComponentModel.DataAnnotations;

namespace LightNap.Core.Identity.Dto.Request
{
    /// <summary>
    /// Data transfer object for requesting a password change.
    /// </summary>
    public class ChangePasswordRequestDto
    {
        /// <summary>
        /// Gets or sets the current password of the user.
        /// </summary>
        [Required]
        [StringLength(Constants.Dto.MaxPasswordLength)]
        public required string CurrentPassword { get; set; }

        /// <summary>
        /// Gets or sets the new password.
        /// </summary>
        [Required]
        [StringLength(Constants.Dto.MaxPasswordLength)]
        public required string NewPassword { get; set; }

        /// <summary>
        /// Gets or sets the confirmation of the new password.
        /// </summary>
        [Required]
        [Compare("NewPassword", ErrorMessage = "Parola noua si parola de confirmare nu coincid.")]
        [StringLength(Constants.Dto.MaxPasswordLength)]
        public required string ConfirmNewPassword { get; set; }
    }
}
