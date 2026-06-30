using System.Linq.Expressions;
using MongoDB.Driver;
using ModsenCatalog.Application.Interfaces;

namespace ModsenCatalog.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    protected BaseRepository(IMongoCollection<T> collection)
    {
        _collection = collection;
    }

    public virtual async Task<T> GetByIdAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public virtual async Task<List<T>> FindAsync(Expression<Func<T, bool>> filter)
    {
        return await _collection.Find(filter).ToListAsync();
    }

    public virtual async Task CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }

    public virtual async Task UpdateAsync(T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", GetIdValue(entity));
        
        await _collection.ReplaceOneAsync(filter, entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        
        await _collection.DeleteOneAsync(filter);
    }

    private object? GetIdValue(T entity)
    {
        var property = typeof(T).GetProperty("Id");
            
        return property?.GetValue(entity);
    }
}