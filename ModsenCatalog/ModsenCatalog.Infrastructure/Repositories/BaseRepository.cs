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

    public virtual T GetById(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public virtual List<T> GetAll()
    {
        return _collection.Find(_ => true).ToList();
    }

    public virtual List<T> Find(Expression<Func<T, bool>> filter)
    {
        return _collection.Find(filter).ToList();
    }

    public virtual void Create(T entity)
    {
        _collection.InsertOne(entity);
    }

    public virtual void Update(T entity)
    {
        var filter = Builders<T>.Filter.Eq("Id", GetIdValue(entity));
        
        _collection.ReplaceOne(filter, entity);
    }

    public virtual void Delete(Guid id)
    {
        var filter = Builders<T>.Filter.Eq("Id", id);
        
        _collection.DeleteOne(filter);
    }

    private object? GetIdValue(T entity)
    {
        var property = typeof(T).GetProperty("Id");
            
        return property?.GetValue(entity);
    }
}