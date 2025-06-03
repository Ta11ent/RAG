using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    internal class Tag
    {
        internal int Id { get; init; }
        internal string Name { get; init; } = string.Empty;
    }
}
