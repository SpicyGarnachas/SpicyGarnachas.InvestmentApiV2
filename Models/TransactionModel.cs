namespace SpicyGarnachas.FinanceApiV2.Models;

public class TransactionModel
{
    public int id { get; set; }
    public int? portfolioId { get; set; }
    public int? referenceId { get; set; }
    public string? type { get; set; } // return, spend, income
    public string? description { get; set; }
    public string? category { get; set; } // dividend, interest, capital gain, deposit, withdrawal
    public int? value { get; set; }
    public string? currencyCode { get; set; }
    public DateTime? createdOn { get; set; }
    public DateTime? updatedOn { get; set; }
}