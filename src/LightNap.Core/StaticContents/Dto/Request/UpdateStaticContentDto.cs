using LightNap.Core.Configuration;
using LightNap.Core.StaticContents.Enums;
using System.ComponentModel.DataAnnotations;

namespace LightNap.Core.StaticContents.Dto.Request
{
    public class UpdateStaticContentDto
    {
        [RegularExpression(@"^[a-z0-9]+(-[a-z0-9]+)*$", ErrorMessage = "Cheia continutului static trebuie sa fie alfanumerica cu litere mici si cratime, nu poate incepe/termina cu cratima si nu poate contine cratime consecutive.")]
        [Length(1, Constants.Dto.MaxStaticContentKeyLength)]
        public required string Key { get; set; }
        public required StaticContentType Type { get; set; }
        public required StaticContentStatus Status { get; set; }
        public required StaticContentReadAccess ReadAccess { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_-]+(,[a-zA-Z0-9_-]+)*$", ErrorMessage = "Rolurile editorului trebuie sa fie o lista separata prin virgula cu nume de rol valide.")]
        [MaxLength(256)]
        public string? EditorRoles { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9_-]+(,[a-zA-Z0-9_-]+)*$", ErrorMessage = "Rolurile vizualizatorului trebuie sa fie o lista separata prin virgula cu nume de rol valide.")]
        [MaxLength(256)]
        public string? ReaderRoles { get; set; }
    }
}