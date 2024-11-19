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
    IOrderRepository orderRepository,
    ILogger<OrderService> logger
): IOrderService
{
    private readonly ApplicationDBContext _context = context;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;

    private readonly ILogger<OrderService> _logger = logger;

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

            _logger.LogInformation("Order created successfully.");

            return true;
        }

        catch (Exception ex)
        {
            // Rollback the transaction
            dbTransaction.Rollback();
            throw new Exception(ex.Message);
        }
    }

    public async Task<Order> GetOne(int id, bool withRelations = false)
    {
        var data = withRelations ? await _orderRepository.FindOneWithRelations(id) : await _orderRepository.FindOneWithoutRelations(id);

        return data;
    }

    public async Task<AsyncVoidMethodBuilder> UpdateOrderStatus(int orderId, string status)
    {
        var order = _context.Orders.Find(orderId) ?? throw new Exception("Order not found.");

        // Check if order status is already the same
        if (order.Status == status)
        {
            throw new Exception("Order status is already " + status);
        }

        /**
         * Check if the status is valid
         * 
         * Shipped status can only be set if the order status is Processed
         * or if the order status is already Shipped, the status can only be set to Completed
         */
        if (status == OrderStatus.Shipped && order.Status != OrderStatus.Processed)
        {
            throw new Exception("Order status is not processed.");
        }

        if (status == OrderStatus.Completed && order.Status != OrderStatus.Shipped)
        {
            throw new Exception("Order status is not shipped.");
        }

        order.Status = status;

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Order with id {orderId} updated to {status} successfully.", orderId, status);

        return AsyncVoidMethodBuilder.Create();
    }
}