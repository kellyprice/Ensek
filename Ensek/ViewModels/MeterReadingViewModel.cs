namespace Ensek.ViewModels
{
    public class MeterReadingViewModel
    {
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public string? MeterReadingValue { get; set; }
        public string? Error { get; set; }
    }
}
