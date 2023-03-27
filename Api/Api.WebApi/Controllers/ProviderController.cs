using Application.Interfaces.Services;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
public class ProviderController : Controller
{
    private readonly IProviderService _providerService;

    public ProviderController(IProviderService providerService)
    {
        _providerService = providerService;
    }

    [HttpGet("provider")]
    [ProducesResponseType(typeof(ProviderViewData[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetProviders()
    {
        var result = await _providerService.GetProviders();

        return Ok(result);
    }
}