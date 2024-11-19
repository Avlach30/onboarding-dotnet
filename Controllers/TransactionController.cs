using Microsoft.AspNetCore.Mvc;
using onboarding_dotnet.Infrastructures.Responses;
using onboarding_dotnet.Interfaces.Services;

namespace onboarding_dotnet.Controllers;

[ApiController]
[Route("transactions")]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;

    [HttpPut("{id:int}/success")]
    public async Task<ActionResult<ApiResponse>> UpdatePaymentStatusToSuccess(int id)
    {
        await _transactionService.UpdatePaymentStatusToSuccess(id);

        return Ok(ApiResponse.Success("Payment status updated to success"));
    }
}