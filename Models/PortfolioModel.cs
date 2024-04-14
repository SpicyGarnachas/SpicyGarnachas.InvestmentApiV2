namespace SpicyGarnachas.InvestmentApiV2.Models;

public record PortfolioModel(int id,
                             int? userId,
                             string? name,
                             string? description,
                             decimal? expenseLimit,
                             string? currencyCode,
                             DateTime? createdOn,
                             DateTime? updatedOn);
