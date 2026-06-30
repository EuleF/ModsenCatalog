using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByUsernameAsync(string username);
    
    Task<User> GetByEmailAsync(string email);
    
    Task<List<User>> GetByRoleAsync(string role);
}