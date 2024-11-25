using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using onboarding_dotnet.Dtos.Index;
using onboarding_dotnet.Dtos.Orders;
using onboarding_dotnet.Infrastructures.Mails.Classes;
using onboarding_dotnet.Infrastructures.Mails.Interfaces;
using onboarding_dotnet.Infrastructures.Repositories;
using onboarding_dotnet.Infrastuctures.Database;
using onboarding_dotnet.Models;
using onboarding_dotnet.Repositories;
using onboarding_dotnet.Utils.Enums;

namespace onboarding_dotnet.Services;

public class OrderService(
    ApplicationDBContext context,
    ProductRepository productRepository,
    OrderRepository orderRepository,
    ILogger<OrderService> logger,
    IEmailService emailService,
    UserRepository userRepository
)
{
    private readonly ApplicationDBContext _context = context;
    private readonly ProductRepository _productRepository = productRepository;
    private readonly OrderRepository _orderRepository = orderRepository;
    private readonly ILogger<OrderService> _logger = logger;
    private readonly IEmailService _emailService = emailService;
    private readonly UserRepository _userRepository = userRepository;

    public async Task<PaginationResult<Order>> GetAllForIndexPage(IndexOrderRequestDto requestDto)
    {
        return await _orderRepository.FindAllForIndex(requestDto);
    }

    public async Task<bool> CreateAsync(OrderRequestDto requestDto, int loggedUserId)
    {
        // Begin transaction
        using var dbTransaction = await _context.Database.BeginTransactionAsync();

        try
        {   
            var loggedUser = await _userRepository.FindByIdAsync(loggedUserId) ?? throw new Exception("User not found.");

            // Initialize the total price
            decimal totalPrice = 0;

            // Initialize the list of OrderProduct
            List<OrderProduct> orderProducts = [];
            List<Product> productsToUpdate = [];

            // Create the order
            var order = new Order
            {
                UserId = loggedUserId,
                Status = OrderStatus.Draft,
                TotalPrice = totalPrice,
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            // Iterate through the order products
            foreach (var orderProduct in requestDto.OrderProducts)
            {
                var product = await _productRepository.FindOneByIdWithoutCategory(orderProduct.ProductId) ?? throw new Exception("Product not found.");

                // Check if the product stock is enough
                if (product.Stock < orderProduct.Quantity)
                {
                    throw new Exception("Product stock is not enough.");
                }

                totalPrice += product.Price * orderProduct.Quantity;

                product.Stock -= orderProduct.Quantity;

                productsToUpdate.Add(product);

                // Push each order product to orderProducts
                orderProducts.Add(new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = orderProduct.ProductId,
                    Quantity = orderProduct.Quantity
                });
            }

            // Update the total price of the order
            order.TotalPrice = totalPrice;

            // Create transaction for the payment
            var transaction = new Transaction
            {
                OrderId = order.Id,
                PaymentMethod = requestDto.PaymentMethod,
                PaymentStatus = PaymentStatus.Pending
            };
            await _context.Transactions.AddAsync(transaction);

            // Add all related entities to the context
            _context.UpdateRange(productsToUpdate);
            await _context.OrderProducts.AddRangeAsync(orderProducts);

            // Commit the transaction
            await _context.SaveChangesAsync();
            await dbTransaction.CommitAsync();

            // Log the success
            _logger.LogInformation("Order created successfully.");

            
            // Send email to the related user
            EmailMetadata emailMetadata = new(
                loggedUser.Email,
                "New Order Created",
                "Dear @Model.Name, your order has been created successfully."
            );

            EmailTemplateModel emailTemplateModel = new(
                loggedUser.Name,
                loggedUser.Email
            );

            // Send the email in a separate thread for performance
            _ = Task.Run(async () =>
            {
                await _emailService.SendUsingTemplate(emailMetadata, emailTemplateModel);
            });

            return true;
        }

        catch (Exception ex)
        {
            // Rollback the transaction
            await dbTransaction.RollbackAsync();
            throw new Exception(ex.Message);
        }
    }

    public async Task<Order> GetOne(int id, bool withRelations = false)
    {
        var data = withRelations ? await _orderRepository.FindOneByIdWithRelations(id) : await _orderRepository.FindOneByIdWithoutRelations(id);

        return data ?? throw new Exception("Data not found.");
    }

    public async Task<AsyncVoidMethodBuilder> UpdateOrderStatus(int orderId, string status)
    {
        var order = _context.Orders
            .Include(order => order.User)
            .AsSplitQuery()
            .FirstOrDefault(order => order.Id == orderId) ?? throw new Exception("Order not found.");

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

        // Log the success
        _logger.LogInformation("Order with id {orderId} updated to {status} successfully.", orderId, status);

        // Send email to the related user
        EmailMetadata emailMetadata = new(
            order.User.Email,
            "Order Updated",
            "Dear @Model.Name, your order has been updated successfully to " + status + "."
        );

        EmailTemplateModel emailTemplateModel = new(    
            order.User.Name,
            order.User.Email
        );

        // Send the email in a separate thread for performance
        _ = Task.Run(async () =>
        {
            await _emailService.SendUsingTemplate(emailMetadata, emailTemplateModel);
        });

        return AsyncVoidMethodBuilder.Create();
    }
}