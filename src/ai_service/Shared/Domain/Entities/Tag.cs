using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain.Entities
{
    [Table("Tags")]
    public class Tag
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
