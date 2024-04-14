namespace SpicyGarnachas.InvestmentApiV2.Models;

public record FinanceValueModel(int id,
                                int? investmentId,
                                decimal? value,
                                string? currencyCode,
                                DateTime? createdOn,
                                DateTime? updatedOn);
