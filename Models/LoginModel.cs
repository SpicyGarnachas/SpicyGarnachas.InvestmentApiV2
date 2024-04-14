namespace SpicyGarnachas.InvestmentApiV2.Models;

public record LoginModel(bool isSuccess,
                         string? message,
                         int id,
                         string? userName,
                         byte[]? image,
                         DateTime? createdOn,
                         DateTime? updatedOn);
