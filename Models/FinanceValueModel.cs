namespace SpicyGarnachas.InvestmentApiV2.Models;

public class FinanceValueModel
{
    public int id { get; set; }
    public int? investmentId { get; set; }
    public decimal? value { get; set; }
    public string? currencyCode { get; set; }
    public DateTime? createdOn { get; set; }
    public DateTime? updatedOn { get; set; }
}