using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Mappers;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrderService orderService): Controller
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> Store([FromBody] OrderRequestDto orderRequestDto)
    {
        var loggedUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _orderService.CreateAsync(orderRequestDto, loggedUserId);

        return Ok(ApiResponse.Success("Order created"));
    
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<ActionResult<ApiResponse<OrderResponseDto>>> Show(int id)
    {
        var order = await _orderService.GetOne(id, true);

        return Ok(ApiResponse<OrderResponseDto>.Success(order.ToResponse(), "Get order success"));
    }
}