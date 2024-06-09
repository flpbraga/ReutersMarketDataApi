namespace ReutersMarketDataApi.Model
{
    public class Price
    {
        public int Id { get; set; } 
        public DateTime UpdateDate { get; set; }
        public decimal Value { get; set; }
        public string Source { get; set; }
        
        public int AssetId { get; set; }
    }
}
