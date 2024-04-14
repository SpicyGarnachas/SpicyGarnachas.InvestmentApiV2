namespace SpicyGarnachas.InvestmentApiV2.Models;

public record FinanceModel(int id,
                           int? portfolioId,
                           string? name,
                           string? clasification,
                           string? description,
                           string? platform,
                           string? type,
                           string? sector,
                           int? risk,
                           int? liquidity,
                           string? currencyCode,
                           DateTime? createdOn,
                           DateTime? updatedOn);
