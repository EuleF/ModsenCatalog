using ModsenCatalog.Domain.Entities;

namespace ModsenCatalog.Application.Interfaces;

public interface IUserRepository : IRepository<User>
{
    User GetByUsername(string username);
    
    User GetByEmail(string email);
    
    List<User> GetByRole(string role);
}