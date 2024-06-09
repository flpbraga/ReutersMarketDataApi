using Microsoft.EntityFrameworkCore;
using ReutersMarketDataApi.Data;
using ReutersMarketDataApi.Model;

namespace ReutersMarketDataApi.Interface
{
    public class AssetRepository : IAssetRepository
    {
        private AssetContext _context;

        public AssetRepository(AssetContext context)
        {
            _context = context;

        }
        public async Task<IEnumerable<Asset>> GetAllAssetsAsync()
        {
            return await _context.Assets.Include(a => a.Prices).ToListAsync();
        }

        public async Task<Asset?> GetAssetById(int id) => await _context.Assets.Include(a => a.Prices).FirstOrDefaultAsync(a => a.Id == id);

        public async Task<IEnumerable<Asset>> GetFilterAssetsAsync(DateTime date, string? source) =>
            await _context.Assets
            .Include(a => a.Prices)
            .Where(a => a.Prices.Any(p => p.UpdateDate.Date == date.Date &&
            (string.IsNullOrEmpty(source) || p.Source.ToLower() == source)))
            .ToListAsync();


        public async Task CreateAsset(Asset asset)
        {
            _context.Assets.Add(asset);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsset(int id, Asset asset)
        {

            _context.Entry(asset).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AssetExists(id))
                {
                    throw new Exception("Asset exist");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool AssetExists(int id)
        {
            return _context.Assets.Any(e => e.Id == id);
        }
    }
}
