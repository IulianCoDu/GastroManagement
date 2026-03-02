using System.Collections.ObjectModel;

namespace LightNap.Core.Configuration
{
    /// <summary>
    /// Configuration for claim-based security.
    /// </summary>
    public static class ClaimSecurityConfig
    {
        /// <summary>
        /// Gets the comma-separated list of roles authorized to manage user claims. This is used to secure the claim management endpoints.
        /// If you add new roles that need to be able to manage user claims, include them here so that they update the controller authorization.
        /// </summary>
        public const string ClaimManagementRoles = $"{Constants.Roles.Administrator},{Constants.Roles.MedicEditor}";

        /// <summary>
        /// The claim security rules that define which roles grant which claims. Any claims not listed here require an admin. If you add
        /// new roles here, be sure to update <see cref="ClaimManagementRoles"/> so users in those roles can pass controller authorization.
        /// </summary>
        internal static IReadOnlyList<ClaimSecurityRule> AllRules =>
        [
            new ClaimSecurityRule(Constants.Claims.MedicEditor, [ApplicationRoles.MedicEditor]),
            new ClaimSecurityRule(Constants.Claims.MedicReader, [ApplicationRoles.MedicEditor]),
        ];

        /// <summary>
        /// Lookup dictionary for claim keys to roles that grant those claims.
        /// </summary>
        internal static ReadOnlyDictionary<string, string[]> RulesLookup { get; } =
            ClaimSecurityConfig.AllRules.ToDictionary(
                rule => rule.ClaimType,
                rule => rule.Roles.Select(role => role.Name!).ToArray()
                ).AsReadOnly();
    }
}
