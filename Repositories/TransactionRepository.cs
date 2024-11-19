using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Models;

namespace onboarding_dotnet.Repositories;

public class TransactionRepository(ApplicationDBContext context): ITransactionRepository
{
    private readonly ApplicationDBContext _context = context;

    public async Task<Transaction> FindOneWithRelations(int id)
    {
        return await _context.Transactions
            .Include(transaction => transaction.Order)
            .FirstOrDefaultAsync(transaction => transaction.Id == id) 
            ?? throw new Exception("Transaction not found");
    }
}