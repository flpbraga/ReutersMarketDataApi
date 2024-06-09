using ReutersMarketDataApi.Model;

namespace ReutersMarketDataApi.Interface
{
    public interface IAssetRepository
    {
        Task<IEnumerable<Asset>> GetAllAssetsAsync();
        Task<Asset> GetAssetById(int id);
        Task<IEnumerable<Asset>> GetFilterAssetsAsync(DateTime date, string? source);
        Task CreateAsset(Asset asset);
        Task UpdateAsset(int id, Asset asset);
    }
}
