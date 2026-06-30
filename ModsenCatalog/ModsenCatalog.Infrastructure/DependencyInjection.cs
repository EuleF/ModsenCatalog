using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModsenCatalog.Application.Handlers;
using MongoDB.Driver;
using ModsenCatalog.Application.Interfaces;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Infrastructure.Data;
using ModsenCatalog.Infrastructure.Events;
using ModsenCatalog.Infrastructure.Options;
using ModsenCatalog.Infrastructure.Repositories;

namespace ModsenCatalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration["MongoDb:ConnectionString"];
        var databaseName = configuration["MongoDb:DatabaseName"];
        
        services.AddSingleton(new MongoDbOptions 
        { 
            ConnectionString = connectionString,
            DatabaseName = databaseName
        });
        
        services.AddSingleton<MongoDbContext>();
            
        services.AddSingleton<IMongoCollection<User>>(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Users;
        });
            
        services.AddSingleton<IMongoCollection<Category>>(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Categories;
        });
            
        services.AddSingleton<IMongoCollection<Product>>(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Products;
        });
            
        services.AddSingleton<IMongoCollection<Review>>(sp =>
        {
            var context = sp.GetRequiredService<MongoDbContext>();
            return context.Reviews;
        });
            
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        
        services.AddSingleton<IEventPublisher, EventPublisher>();
        services.AddSingleton<EventHandlerService>();

        return services;
    }
}