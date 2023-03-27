using Application.Interfaces.Services;
using Domain.Dto;
using Domain.FilterModels;
using Domain.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[EnableCors("CorsPolicy")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Получить отфильтрованные заказы
    /// </summary>
    [HttpGet("filtered-order")]
    [ProducesResponseType(typeof(OrderViewData[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetOrders([FromQuery] OrderFilterModel filterModel)
    {
        var query = await _orderService.GetOrders(filterModel);

        return Ok(query);
    }

    /// <summary>
    /// Получить заказы
    /// </summary>
    [HttpGet("order")]
    [ProducesResponseType(typeof(OrderViewData[]), 200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetOrders()
    {
        var query = await _orderService.GetOrders();

        return Ok(query);
    }

    /// <summary>
    /// Добавить заказ
    /// </summary>
    [HttpPost("order")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> AddOrder([FromBody] OrderData order)
    {
        await _orderService.AddOrder(order);

        return NoContent();
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    [HttpPut("order")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> UpdateOrder([FromBody] OrderViewData order)
    {
        await _orderService.UpdateOrder(order);
        
        return NoContent();
    }

    /// <summary>
    /// Удалить строку из заказа
    /// </summary>
    [HttpDelete("order-item")]
    [ProducesResponseType(204)]
    [AllowAnonymous]
    public async Task<IActionResult> DeleteOrderItem([FromBody] int orderItemId)
    {
        await _orderService.DeleteOrderItem(orderItemId);

        return NoContent();
    }

    /// <summary>
    /// Получить данные для фильтрации заказов
    /// </summary>
    [HttpGet("filter-model")]
    [ProducesResponseType(typeof(OrderFilterModel),200)]
    [AllowAnonymous]
    public async Task<IActionResult> GetDataForFilterOrder()
    {
        var filterModel = await _orderService.GetDataForFilterOrder();

        return Ok(filterModel);
    }
}