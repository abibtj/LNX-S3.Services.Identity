using S3.Services.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S3.Services.Identity.Services
{
    public interface IClaimsProvider
    {
         Task<IDictionary<string, string>> GetAsync(User user);
         //Task<IDictionary<string, string>> GetAsync(Guid userId);
    }
}