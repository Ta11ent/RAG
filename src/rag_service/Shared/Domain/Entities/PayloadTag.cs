using System.ComponentModel.DataAnnotations.Schema;

namespace AI_service.Shared.Domain.Entities
{
    [Table("PayloadTags")]
    public class PayloadTag
    {
        public Guid PayloadId { get; init; }
        public Guid TagId { get; init; }

        public static PayloadTag Create(
            Guid payloadId,
            Guid tagId)
        {
            return new PayloadTag()
            {
                PayloadId = payloadId,
                TagId = tagId
            };
        }
    }
}
