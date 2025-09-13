using Ml;

namespace AI_service.Shared.Services.ml_service
{
    public interface IMlServiceClient
    {
        Task<TrainResponse> TrainAsync(IEnumerable<DataEntry> entries);
        Task<SearchResponse> SearchAsync(string query, IEnumerable<string> tags, int topK);
    }
}
