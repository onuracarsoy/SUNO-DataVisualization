using AutoMapper;
using DataVisualizationUI.Context;
using DataVisualizationUI.Dtos;
using DataVisualizationUI.Entities;
using DataVisualizationUI.Models;
using Microsoft.EntityFrameworkCore;

namespace DataVisualizationUI.Services
{
    public class ReviewService(DataVisualizationDbContext context, IMapper mapper)
    {
      
        public async Task<PagedViewModel<ReviewListDto>> ReviewListAsync(int page = 1, int pageSize = 10, string search = "")
        {
            var query = context.Reviews
                .Include(r => r.Product)
                .Include(r => r.Customer)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(r =>
                    (r.Product != null && r.Product.ProductName.Contains(search))
                    || (r.Customer != null && (r.Customer.CustomerName.Contains(search) || r.Customer.CustomerSurname.Contains(search)))
                    || r.ReviewText.Contains(search));
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(r => r.ReviewDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var reviewDtos = mapper.Map<List<ReviewListDto>>(items);

            return new PagedViewModel<ReviewListDto>
            {
                Items = reviewDtos,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

  
        public async Task<Review?> GetReviewByIdAsync(int id)
        {
            return await context.Reviews
                .Include(r => r.Product)
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(r => r.ReviewId == id);
        }

      
        public async Task CreateReviewAsync(CreateReviewDto dto)
        {
            var review = mapper.Map<Review>(dto);
            await context.Reviews.AddAsync(review);
            await context.SaveChangesAsync();
        }

    
        public async Task UpdateReviewAsync(UpdateReviewDto dto)
        {
            var review = await context.Reviews.FindAsync(dto.ReviewId);
            if (review == null)
                throw new KeyNotFoundException($"Review with ID {dto.ReviewId} not found");

            mapper.Map(dto, review);
            await context.SaveChangesAsync();
        }

    
        public async Task DeleteReviewAsync(int id)
        {
            var review = await context.Reviews.FindAsync(id);
            if (review != null)
            {
                context.Reviews.Remove(review);
                await context.SaveChangesAsync();
            }
        }
    }
}
