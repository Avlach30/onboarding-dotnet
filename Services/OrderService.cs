using System.Runtime.CompilerServices;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Interfaces.Repositories;
using onboarding_dotnet.Interfaces.Services;
using onboarding_dotnet.Models;
using onboarding_dotnet.Utils.Enums;

namespace onboarding_dotnet.Services;

public class OrderService(
    ApplicationDBContext context,
    IProductRepository productRepository,
    IUserRepository userRepository
): IOrderService
{
    private readonly IUserRepository _userRepository = userRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ApplicationDBContext _context = context;

    public async Task<bool> CreateAsync(OrderRequestDto requestDto, int loggedUserId)
    {
        // Begin transaction
        var dbTransaction = _context.Database.BeginTransaction();

        try
        {   
            // Initialize the total price
            decimal totalPrice = 0;

            // Initialize the list of OrderProduct
            List<OrderProduct> orderProducts = [];

            // Iterate through the order products
            foreach (var orderProduct in requestDto.OrderProducts)
            {
                var product = await _productRepository.FindOne(orderProduct.ProductId) ?? throw new Exception("Product not found.");

                totalPrice += product.Price * orderProduct.Quantity;

                // Check if the product stock is enough
                if (product.Stock < orderProduct.Quantity)
                {
                    throw new Exception("Product stock is not enough.");
                }

                product.Stock -= orderProduct.Quantity;
                await _productRepository.UpdateAsync(product);

                // Push each order product to orderProducts
                orderProducts.Add(new OrderProduct
                {
                    ProductId = orderProduct.ProductId,
                    Quantity = orderProduct.Quantity
                });
            }

            // Create the order
            var order = new Order
            {
                UserId = loggedUserId,
                Status = OrderStatus.Draft,
                TotalPrice = totalPrice,
            };

            // Add the order to the context
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Improve orderProducts
            orderProducts.ForEach(orderProduct =>
            {
                orderProduct.OrderId = order.Id;
            });

            // Add the order products to the context
            await _context.OrderProducts.AddRangeAsync(orderProducts);
            await _context.SaveChangesAsync();

            // Create transaction for the payment
            var transaction = new Transaction
            {
                OrderId = order.Id,
                PaymentMethod = requestDto.PaymentMethod,
                PaymentStatus = PaymentStatus.Pending
            };

            // Add the transaction to the context
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            // Commit the transaction
            dbTransaction.Commit();

            return true;
        }

        catch (Exception ex)
        {
            // Rollback the transaction
            dbTransaction.Rollback();
            throw new Exception(ex.Message);
        }
    }

    public async Task<AsyncVoidMethodBuilder> UpdateOrderStatus(int orderId, string status)
    {
        var order = _context.Orders.Find(orderId) ?? throw new Exception("Order not found.");

        order.Status = status;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return AsyncVoidMethodBuilder.Create();
    }
}