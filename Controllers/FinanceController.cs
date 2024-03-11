using Microsoft.AspNetCore.Mvc;
using SpicyGarnachas.InvestmentApiV2.Services.Interfaces;
using SpicyGarnachas.InvestmentApiV2.Models;
using Microsoft.AspNetCore.Cors;

namespace SpicyGarnachas.InvestmentApiV2.Controllers;


[Route("api/[controller]")]
[ApiController]
public class FinanceController : ControllerBase
{
    private readonly IFinanceService services;

    public FinanceController(IFinanceService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("GetFinanceData")]
    public async Task<ActionResult<IEnumerable<FinanceModel>?>> GetFinanceData()
    {
        var (IsSuccess, Result, Message) = await services.GetFinanceData();
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }
    
    [HttpGet]
    [Route("GetFinanceDataByPortfolioId")]
    public async Task<ActionResult<IEnumerable<FinanceModel>?>> GetFinanceDataByPortfolioId(int id)
    {
        var (IsSuccess, Result, Message) = await services.GetFinanceDataByPortfolioId(id);
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }

    [HttpPost]
    [Route("CreateNewFinance")]
    public async Task<ActionResult<string>> CreateNewFinance(FinanceModel investment)
    {
        var (IsSuccess, Message) = await services.CreateNewFinance(investment);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }

    [HttpPut]
    [Route("ModifyFinance")]
    public async Task<ActionResult<string>> ModifyFinance(FinanceModel investment)
    {
        var (IsSuccess, Message) = await services.ModifyFinance(investment);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }

    [HttpDelete]
    [Route("DeleteFinance")]
    public async Task<ActionResult<string>> DeleteFinance(int id, int portfolioId)
    {
        var (IsSuccess, Message) = await services.DeleteFinance(id, portfolioId);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }
}