using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    [Table("Tags")]
    internal class Tag
    {
        internal int Id { get; init; }
        internal string Name { get; init; } = string.Empty;
    }
}
