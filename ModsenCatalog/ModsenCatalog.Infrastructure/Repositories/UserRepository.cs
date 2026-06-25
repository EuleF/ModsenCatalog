using MongoDB.Driver;
using ModsenCatalog.Domain.Entities;
using ModsenCatalog.Application.Interfaces;
using ModsenCatalog.Domain.Enums;

namespace ModsenCatalog.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(IMongoCollection<User> collection) : base(collection)
    {
    }

    public User GetByUsername(string username)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public User GetByEmail(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        
        return _collection.Find(filter).FirstOrDefault();
    }

    public List<User> GetByRole(string role)
    {
        var roleEnum = Enum.Parse<UserRole>(role, true);
        var filter = Builders<User>.Filter.Eq(u => u.Role, roleEnum);
        
        return _collection.Find(filter).ToList();
    }
}