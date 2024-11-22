using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Services;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController(TransactionService transactionService) : ControllerBase
{
    private readonly TransactionService _transactionService = transactionService;

    [HttpPut("{id:int}/success")]
    public async Task<ActionResult<ApiResponse>> UpdatePaymentStatusToSuccess(int id)
    {
        await _transactionService.UpdatePaymentStatusToSuccess(id);

        return Ok(ApiResponse.Success("Payment status updated to success"));
    }
}