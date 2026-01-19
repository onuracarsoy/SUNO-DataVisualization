using AutoMapper;
using DataVisualizationUI.Context;
using DataVisualizationUI.Dtos;
using DataVisualizationUI.Entities;
using DataVisualizationUI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataVisualizationUI.Services
{
    public class CustomerService(DataVisualizationDbContext context, IMapper mapper)
    {
        
        public async Task<PagedViewModel<CustomerListDto>> CustomerListAsync(int page = 1, string search = "")
        {
            int pageSize = 10;
            var query = context.Customers
                .Include(c => c.Orders)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.CustomerName.Contains(search)
                    || c.CustomerSurname.Contains(search)
                    || c.CustomerEmail.Contains(search));
            }

            var totalItems = await query.CountAsync();
            var customers = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            var customerDtos = mapper.Map<List<CustomerListDto>>(customers);

            return new PagedViewModel<CustomerListDto>
            {
                Items = customerDtos,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

      
        public async Task<UpdateCustomerDto?> GetByIdCustomerAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
                return null;

            return mapper.Map<UpdateCustomerDto>(customer);
        }

     
        public async Task CreateCustomerAsync(CreateCustomerDto dto)
        {
            var customer = mapper.Map<Customer>(dto);
            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
        }

      
        public async Task UpdateCustomerAsync(UpdateCustomerDto dto)
        {
            var customer = await context.Customers.FindAsync(dto.CustomerId);
            if (customer == null)
                throw new KeyNotFoundException($"Customer with ID {dto.CustomerId} not found");

            mapper.Map(dto, customer);
            await context.SaveChangesAsync();
        }

     
        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer != null)
            {
                context.Customers.Remove(customer);
                await context.SaveChangesAsync();
            }
        }
    }
}
