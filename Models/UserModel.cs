namespace SpicyGarnachas.InvestmentApiV2.Models;

public class UserModel
{
    public int id { get; set; }
    public string? userName { get; set; }
    public string? password { get; set; }
    public string? salt { get; set; }
    public byte[]? image { get; set; }
    public DateTime? createdOn { get; set; }
    public DateTime? updatedOn { get; set; }
}