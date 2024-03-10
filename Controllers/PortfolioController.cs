using Microsoft.AspNetCore.Mvc;
using SpicyGarnachas.FinanceApiV2.Services.Interfaces;
using SpicyGarnachas.FinanceApiV2.Models;
using Microsoft.AspNetCore.Cors;

namespace SpicyGarnachas.FinanceApiV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PortfolioController : ControllerBase
{
    private readonly IPortfolioService services;

    public PortfolioController(IPortfolioService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("GetPortfolioData")]
    public async Task<ActionResult<IEnumerable<PortfolioModel>?>> GetPortfolioData()
    {
        var (IsSuccess, Result, Message) = await services.GetPortfolioData();
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }

    [HttpGet]
    [Route("GetPortfolioByUserId")]
    public async Task<ActionResult<IEnumerable<PortfolioModel>?>> GetPortfolioByUserId(int id)
    {
        var (IsSuccess, Result, Message) = await services.GetPortfolioByUserId(id);
        return IsSuccess ? Ok(Result) : BadRequest(Message);
    }

    [HttpPost]
    [Route("CreateNewPortfolio")]
    public async Task<ActionResult<string>> CreateNewPortfolio(PortfolioModel portfolio)
    {
        var (IsSuccess, Message) = await services.CreateNewPortfolio(portfolio);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }

    [HttpPut]
    [Route("ModifyPorfolio")]
    public async Task<ActionResult<string>> ModifyPorfolio(PortfolioModel portfolio)
    {
        var (IsSuccess, Message) = await services.ModifyPorfolio(portfolio);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }

    [HttpDelete]
    [Route("DeletePortfolio")]
    public async Task<ActionResult<string>> DeletePortfolio(int id, int userId)
    {
        var (IsSuccess, Message) = await services.DeletePortfolio(id, userId);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }
}