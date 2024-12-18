using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Mappers;
using onboarding_dotnet.Services;
using onboarding_dotnet.Utils.Enums;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("orders")]
public class OrderController(
    OrderService orderService
): Controller
{
    private readonly OrderService _orderService = orderService;

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IndexResponse<OrderDto>>> Index(
        [FromQuery] IndexOrderRequestDto request
    )
    {
        var result = await _orderService.GetAllForIndexPage(request);

        return Ok(IndexResponse<OrderDto>.Success(
            result.Data.Select(order => order.ToDto()).ToList(),
            result.Total,
            "Get orders success",
            request.Page,
            request.PerPage
        ));
    }

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

    [HttpPut("{id:int}/shipping")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> UpdateShippingStatus(int id)
    {
        await _orderService.UpdateOrderStatus(id, OrderStatus.Shipped);

        return Ok(ApiResponse.Success("Order shipping status updated"));
    }

    [HttpPut("{id:int}/completed")]
    [Authorize]
    public async Task<ActionResult<ApiResponse>> UpdateCompletedStatus(int id)
    {
        await _orderService.UpdateOrderStatus(id, OrderStatus.Completed);

        return Ok(ApiResponse.Success("Order completed status updated"));
    }
}