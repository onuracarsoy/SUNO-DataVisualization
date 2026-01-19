using AutoMapper;
using DataVisualizationUI.Entities;

namespace DataVisualizationUI.Dtos.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Category Mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Customer Mappings
            CreateMap<Customer, CustomerDto>();
            CreateMap<Customer, UpdateCustomerDto>();
            CreateMap<Customer, CustomerListDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.CustomerName} {src.CustomerSurname}"))
                .ForMember(dest => dest.TotalOrders, opt => opt.MapFrom(src => src.Orders != null ? src.Orders.Count : 0));
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            // Order Mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products != null ? src.Products.ProductName : string.Empty))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customers != null ? $"{src.Customers.CustomerName} {src.Customers.CustomerSurname}" : string.Empty))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Products != null ? src.Products.UnitPrice * src.Quantity : 0));
            CreateMap<Order, UpdateOrderDto>();
            CreateMap<Order, OrderListDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Products != null ? src.Products.ProductName : string.Empty))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customers != null ? $"{src.Customers.CustomerName} {src.Customers.CustomerSurname}" : string.Empty))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.Products != null ? src.Products.UnitPrice * src.Quantity : 0));
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderDto, Order>();

            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty));
            CreateMap<Product, UpdateProductDto>();
            CreateMap<Product, ProductListDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : string.Empty))
                .ForMember(dest => dest.TotalOrders, opt => opt.MapFrom(src => src.Orders != null ? src.Orders.Count : 0))
                .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.Reviews != null && src.Reviews.Any() ? src.Reviews.Average(r => r.Rating) : 0));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Review Mappings
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.CustomerName} {src.Customer.CustomerSurname}" : string.Empty));
            CreateMap<Review, UpdateReviewDto>();
            CreateMap<Review, ReviewListDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.ProductName : string.Empty))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.CustomerName} {src.Customer.CustomerSurname}" : string.Empty));
            CreateMap<CreateReviewDto, Review>();
            CreateMap<UpdateReviewDto, Review>();
        }
    }
}
