namespace DbOperationwithEFcoreApp.Data
{
    public class CurrencyType
    {
        public int Id { get; set; }
        public string CurrencyName { get; set; }
        public string Description { get; set; }

        public ICollection<BookPrice> BookPrices { get; set; }

    }
}
