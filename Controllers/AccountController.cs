using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using Evento.Infrastructure.Services;
using Evento.Infrastructure.Commands.Users;
using Microsoft.AspNetCore.Authorization;
using Evento.Api.Controllers;


public class AccountController : ApiControllerBase
{
    private readonly IUserService _userService;
    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Json(await _userService.GetAccountAsync(UserId));
    }

    [HttpGet("tickets")]
    public async Task<IActionResult> GetTickets()
    {
        return Json(await _userService.GetAccountAsync(UserId));
    }
    [HttpPost("register")]
    public async Task<IActionResult> Post([FromBody]Register command)
    {
        await _userService.RegisterAsync(Guid.NewGuid(), command.Email, command.Name, command.Password);

        return Created("/account", null);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Post([FromBody]Login command)
    {
        return Json(await _userService.LoginAsyc(command.Email, command.Password));
    }

}