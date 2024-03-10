namespace SpicyGarnachas.FinanceApiV2.Models;

public class FinanceModel
{
    public int id { get; set; }
    public int? portfolioId { get; set; }
    public string? name { get; set; }
    public string? clasification { get; set; }
    public string? description { get; set; }
    public string? platform { get; set; } // etoro, coinbase, binance, BBVA, BRIQ, SNOWBALL, LENDERA, cetesdirecto, GBM+
    public string? type { get; set; } // stock, bond, fund, crypto, real estate, business, collectible, bank account, other
    public string? sector { get; set; } // communication services, consumer discretionary, consumer staples, energy, financials, health care, industrials, information technology, materials, real estate, utilities
    public int? risk { get; set; } // 1 - low (bonds, bank, other) , 2 - medium (stock, fund, real state, collectible, other), 3 - high (crypto, business, other)
    public int? liquidity { get; set; } // 1 - money available in 1 day, 2 - money available in 1 week, 3 - money available in 1 month, 4 - money available in 1 year, 5 - money available in 5 years or more
    public string? currencyCode { get; set; }
    public DateTime? createdOn { get; set; }
    public DateTime? updatedOn { get; set; }
}