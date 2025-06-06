using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain
{
    [Table("Texts")]
    public class Text
    {
        public Guid Id { get; init; }
        public string Content { get; init; } = string.Empty;

        public static Text Create(string content)
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Content = content
            };
        }
    }
}
