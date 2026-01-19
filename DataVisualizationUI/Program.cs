using DataVisualizationUI.Context;
using DataVisualizationUI.Dtos.Mapping;
using DataVisualizationUI.Endpoints;
using DataVisualizationUI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Database Context Configuration
builder.Services.AddDbContext<DataVisualizationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Semantic Kernel Configuration
builder.Services.AddScoped<Kernel>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var aiConfig = config.GetSection("OpenRouterAI");
    return Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(
            modelId: aiConfig["Model"],
            apiKey: aiConfig["ApiKey"],
            endpoint: new Uri(aiConfig["Endpoint"]))
        .Build();
});


// ML.NET Context
builder.Services.AddScoped<MLContext>();

#region Services Registration
builder.Services.AddScoped<DashboardService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ReviewService>();
builder.Services.AddScoped<ForecastService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();
#endregion

// AutoMapper Configuration
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>(), typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();


// Dashboard SSE Endpoints (Minimal API)
app.MapDashboardEndpoints();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
