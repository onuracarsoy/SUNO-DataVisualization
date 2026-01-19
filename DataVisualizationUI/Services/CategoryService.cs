using AutoMapper;
using DataVisualizationUI.Context;
using DataVisualizationUI.Dtos;
using DataVisualizationUI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataVisualizationUI.Services
{
    public class CategoryService(DataVisualizationDbContext context, IMapper mapper)
    {
   
        public async Task<List<CategoryDto>> CategoryListAsync(string search = "")
        {
            var query = context.Categories.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c => c.CategoryName.Contains(search));
            }
            
            var categories = await query.ToListAsync();
            return mapper.Map<List<CategoryDto>>(categories);
        }

     
        public async Task<UpdateCategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category == null)
                return null;

            return mapper.Map<UpdateCategoryDto>(category);
        }

     
        public async Task CreateCategoryAsync(CreateCategoryDto dto)
        {
            var category = mapper.Map<Category>(dto);
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }

     
        public async Task UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            var category = await context.Categories.FindAsync(dto.CategoryId);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {dto.CategoryId} not found");

            mapper.Map(dto, category);
            await context.SaveChangesAsync();
        }


        public async Task DeleteCategoryAsync(int id)
        {
            var category = await context.Categories.FindAsync(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
            }
        }
    }
}
