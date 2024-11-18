using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Interfaces.Services;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(IOrderService orderService): Controller
{
    private readonly IOrderService _orderService = orderService;

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> CreateOrder([FromBody] OrderRequestDto orderRequestDto)
    {
        var loggedUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        await _orderService.CreateAsync(orderRequestDto, loggedUserId);

        return Ok(ApiResponse.Success("Order created"));
    }
}