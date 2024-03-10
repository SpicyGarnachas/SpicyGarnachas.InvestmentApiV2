using SpicyGarnachas.FinanceApiV2.Models;
using Microsoft.AspNetCore.Mvc;
using SpicyGarnachas.FinanceApiV2.Services.Interfaces;
using Microsoft.AspNetCore.Cors;

namespace SpicyGarnachas.FinanceApiV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService services;

    public TransactionController(ITransactionService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("GetTransactionsData")]
    public async Task<ActionResult<IEnumerable<TransactionModel>?>> GetTransactionsData()
    {
        var (IsSuccess, Result, Message) = await services.GetTransactionsData();
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }

    [HttpGet]
    [Route("GetTransactionsDataByPortfolioId")]
    public async Task<ActionResult<IEnumerable<TransactionModel>?>> GetTransactionsDataByPortfolioId(int id)
    {
        var (IsSuccess, Result, Message) = await services.GetTransactionsDataByPortfolioId(id);
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }
}