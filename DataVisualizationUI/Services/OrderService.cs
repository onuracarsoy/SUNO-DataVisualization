using AutoMapper;
using DataVisualizationUI.Context;
using DataVisualizationUI.Dtos;
using DataVisualizationUI.Entities;
using DataVisualizationUI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataVisualizationUI.Services
{
    public class OrderService(DataVisualizationDbContext context, IMapper mapper)
    {
        public async Task<PagedViewModel<OrderListDto>> OrderListAsync(int page = 1, string search = "")
        {
            int pageSize = 10;
            var query = context.Orders
                .Include(p => p.Customers)
                .Include(p => p.Products)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(o =>
                    (o.Customers != null && (o.Customers.CustomerName.Contains(search) || o.Customers.CustomerSurname.Contains(search)))
                    || (o.Products != null && o.Products.ProductName.Contains(search))
                    || o.OrderStatus.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var orders = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            var orderDtos = mapper.Map<List<OrderListDto>>(orders);

            return new PagedViewModel<OrderListDto>
            {
                Items = orderDtos,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await context.Orders
                .Include(o => o.Customers)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task<UpdateOrderDto?> GetOrderForUpdateAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
                return null;

            return mapper.Map<UpdateOrderDto>(order);
        }

        public async Task CreateOrderAsync(CreateOrderDto dto)
        {
            var order = mapper.Map<Order>(dto);
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(UpdateOrderDto dto)
        {
            var order = await context.Orders.FindAsync(dto.OrderId);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {dto.OrderId} not found");

            mapper.Map(dto, order);
            await context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order != null)
            {
                context.Orders.Remove(order);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await context.Products.ToListAsync();
        }
    }
}
