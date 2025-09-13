namespace AI_service.Shared.Outcome
{
    internal static class DomainErrors
    {
        internal static class Tag
        {
            internal static Error NotFound(Guid Id) => Error.NotFound(
                "Tag.Notfound",
                $"The Tag item with the Id = '{Id}' was not found");

            internal static Error NotFound(Guid[] ids) => Error.NotFound(
                "Tags.NotFound",
                $"The Tag items with the Ids = '{ids}' were not found");
        }
    }
}
