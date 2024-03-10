namespace SpicyGarnachas.FinanceApiV2.Models;

public class LoginModel
{
    public bool isSuccess { get; set; }
    public string? message { get; set; }
    public int id { get; set; }
    public string? userName { get; set; }
    public byte[]? image { get; set; }
    public DateTime? createdOn { get; set; }
    public DateTime? updatedOn { get; set; }
}