using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    internal class Text
    {
        internal int Id { get; init; }
        internal string Content { get; init; } = string.Empty;
    }
}
