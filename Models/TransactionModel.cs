namespace SpicyGarnachas.InvestmentApiV2.Models;

public record TransactionModel(int id,
                               int? portfolioId,
                               int? referenceId,
                               string? type,
                               string? description,
                               string? category,
                               decimal? value,
                               string? currencyCode,
                               DateTime? createdOn,
                               DateTime? updatedOn);
