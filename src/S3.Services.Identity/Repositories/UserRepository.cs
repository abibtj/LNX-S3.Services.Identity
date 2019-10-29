using System;
using System.Threading.Tasks;
using S3.Common.Mongo;
using S3.Services.Identity.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq;
using S3.Common.Utility;

namespace S3.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User> _repository;

        public UserRepository(IMongoRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<User> GetAsync(Guid id)
            => await _repository.GetAsync(id);

        public async Task<User> GetAsync(string username)
            => await _repository.GetAsync(x => x.Username == username);

        public async Task AddAsync(User user)
            => await _repository.AddAsync(user);

        public async Task UpdateAsync(User user)
            => await _repository.UpdateAsync(user);

        public async Task RemoveAsync(Guid id)
            => await _repository.DeleteAsync(id);

        public async Task<bool> SchoolAdminExistsAsync(Guid schoolId)
           => await _repository.ExistsAsync(x => x.SchoolId == schoolId && (x.Roles.ToList().Contains(Role.SchoolAdmin)));

    }
}