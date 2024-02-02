using Microsoft.AspNetCore.Mvc;
using SpicyGarnachas.InvestmentApiV2.Services.Interfaces;
using SpicyGarnachas.InvestmentApiV2.Models;
using Microsoft.AspNetCore.Cors;

namespace SpicyGarnachas.InvestmentApiV2.Controllers;


[Route("api/[controller]")]
[ApiController]
public class InvestmentController : ControllerBase
{
    private readonly IInvestmentService services;

    public InvestmentController(IInvestmentService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("GetInvestmentData")]
    public async Task<ActionResult<IEnumerable<InvestmentModel>?>> GetInvestmentData()
    {
        var (IsSuccess, Result, Message) = await services.GetInvestmentData();
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }
    
    [HttpGet]
    [Route("GetInvestmentDataByPortfolioId")]
    public async Task<ActionResult<IEnumerable<InvestmentModel>?>> GetInvestmentDataByPortfolioId(int id)
    {
        var (IsSuccess, Result, Message) = await services.GetInvestmentDataByPortfolioId(id);
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }

    [HttpPost]
    [Route("CreateNewInvestment)")]
    public async Task<ActionResult<string>> CreateNewInvestment(InvestmentModel investment)
    {
        var (IsSuccess, Message) = await services.CreateNewInvestment(investment);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }

    [HttpPut]
    [Route("ModifyInvestment")]
    public async Task<ActionResult<string>> ModifyInvestment(InvestmentModel investment)
    {
        var (IsSuccess, Message) = await services.ModifyInvestment(investment);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }

    [HttpDelete]
    [Route("DeleteInvestment")]
    public async Task<ActionResult<string>> DeleteInvestment(int id, int portfolioId)
    {
        var (IsSuccess, Message) = await services.DeleteInvestment(id, portfolioId);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }
}