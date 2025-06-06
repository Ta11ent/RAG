using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    [Table("Tags")]
    public class Tag
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
