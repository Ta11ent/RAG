using Ml;

namespace AI_service.Shared.Services.ml_service
{
    internal sealed class MlServiceClient : IMlServiceClient
    {
        private readonly Vector.VectorClient _client;

        public MlServiceClient(Vector.VectorClient client) =>
            _client = client ?? throw new ArgumentNullException(nameof(client));

        public async Task<SearchResponse> SearchAsync(string query, IEnumerable<string> tags, int topK)
        {
            SearchRequest request = new()
            {
                Query = query,
                TopK = topK
            };
            request.Tags.AddRange(tags);

            return await _client.SearchAsync(request);
        }

        public async Task<TrainResponse> TrainAsync(IEnumerable<DataEntry> entries)
        {
            var request = new TrainRequest();
            request.Entries.AddRange(entries);

            return await _client.TrainAsync(request);
        }
    }
}
