namespace SpicyGarnachas.InvestmentApiV2.Models;

public record UserModel(int id,
                        string? userName,
                        string? password,
                        string? salt,
                        byte[]? image,
                        DateTime? createdOn,
                        DateTime? updatedOn);
