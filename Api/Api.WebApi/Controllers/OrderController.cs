using Application.Interfaces.Services;
using Domain.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
public class OrderController : Controller
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet("order")]
    [ProducesResponseType(200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetOrders()
    {
        var query = await _service.GetOrders();

        return Ok(query);
    }

    [HttpPost("order")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddOrder(OrderData order)
    {
        await _service.AddOrder(order);

        return NoContent();
    }
}