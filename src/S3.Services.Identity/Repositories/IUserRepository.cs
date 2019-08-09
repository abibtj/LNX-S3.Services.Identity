using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using S3.Services.Identity.Domain;

namespace S3.Services.Identity.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
