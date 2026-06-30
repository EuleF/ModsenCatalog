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

    public async Task<User> GetByUsernameAsync(string username)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Username, username);
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var filter = Builders<User>.Filter.Eq(u => u.Email, email);
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<User>> GetByRoleAsync(string role)
    {
        var roleEnum = Enum.Parse<UserRole>(role, true);
        var filter = Builders<User>.Filter.Eq(u => u.Role, roleEnum);
        
        return await _collection.Find(filter).ToListAsync();
    }
}