using System.ComponentModel.DataAnnotations;

namespace ReutersMarketDataApi.Model
{
    public class Asset
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string ISIN { get; set; }

        public ICollection<Price> Prices { get; set; } = [];
    }
}