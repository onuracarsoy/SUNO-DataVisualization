using AutoMapper;
using DataVisualizationUI.Context;
using DataVisualizationUI.Dtos;
using DataVisualizationUI.Entities;
using DataVisualizationUI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataVisualizationUI.Services
{
    public class ProductService(DataVisualizationDbContext context, IMapper mapper)
    {
      
        public async Task<PagedViewModel<ProductListDto>> ProductListAsync(int page = 1, string search = "")
        {
            int pageSize = 10;
            var query = context.Products
                .Include(p => p.Category)
                .Include(p => p.Orders)
                .Include(p => p.Reviews)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.ProductName.Contains(search)
                    || (p.Category != null && p.Category.CategoryName.Contains(search)));
            }

            var totalItems = await query.CountAsync();
            var products = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            var productDtos = mapper.Map<List<ProductListDto>>(products);

            return new PagedViewModel<ProductListDto>
            {
                Items = productDtos,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

      
        public async Task<PagedViewModel<ReviewListDto>> GetProductReviewsAsync(int productId, int page = 1)
        {
            int pageSize = 10;
            
            var query = context.Reviews
                .Include(r => r.Product)
                .Include(r => r.Customer)
                .Where(p => p.ProductId == productId)
                .OrderByDescending(r => r.ReviewDate)
                .AsQueryable();

            var totalItems = await query.CountAsync();
            var reviews = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            
            var reviewDtos = mapper.Map<List<ReviewListDto>>(reviews);

            return new PagedViewModel<ReviewListDto>
            {
                Items = reviewDtos,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

     
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products
                .Include(p => p.Category)
                .Include(p => p.Orders)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

     
        public async Task<UpdateProductDto?> GetProductForUpdateAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
                return null;

            return mapper.Map<UpdateProductDto>(product);
        }

       
        public async Task CreateProductAsync(CreateProductDto dto)
        {
            var product = mapper.Map<Product>(dto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

      
        public async Task UpdateProductAsync(UpdateProductDto dto)
        {
            var product = await context.Products.FindAsync(dto.ProductId);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {dto.ProductId} not found");

            mapper.Map(dto, product);
            await context.SaveChangesAsync();
        }

       
        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product != null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

     
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
