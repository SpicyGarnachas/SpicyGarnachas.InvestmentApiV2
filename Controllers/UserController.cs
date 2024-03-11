using SpicyGarnachas.InvestmentApiV2.Models;
using Microsoft.AspNetCore.Mvc;
using SpicyGarnachas.InvestmentApiV2.Services.Interfaces;

namespace SpicyGarnachas.InvestmentApiV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService services;

    public UserController(IUserService services)
    {
        this.services = services;
    }

    [HttpGet]
    [Route("Login")]
    public async Task<ActionResult<string>?> Login(string username, string password)
    {
        var (IsSuccess, Message) = await services.Login(username, password);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }

    [HttpPost]
    [Route("Register")]
    public async Task<ActionResult<string>?> Register(UserModel user)
    {
        var (IsSuccess, Message) = await services.Register(user);
        return IsSuccess ? Ok(Message) : BadRequest(Message);
    }
}