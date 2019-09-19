using S3.Services.Identity.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace S3.Services.Identity.Services
{
    public class ClaimsProvider : IClaimsProvider
    {
        public async Task<IDictionary<string, string>> GetAsync(User user)
        {
            return await Task.FromResult(new Dictionary<string, string>
            {
                ["username"] = user.Username,
                ["schoolId"] = user.SchoolId.ToString()
            });
        }
    }
}


//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace S3.Services.Identity.Services
//{
//    public class ClaimsProvider : IClaimsProvider
//    {
//        public async Task<IDictionary<string, string>> GetAsync(Guid userId)
//        {
//            // Provide your own claims collection if needed.
//            // return await Task.FromResult(new Dictionary<string, string>
//            // {
//            //     ["custom_claim_1"] = "value1, value2, value3",
//            //     ["custom_claim_2"] = "value1, value2, value3",
//            // });
//            return await Task.FromResult(new Dictionary<string, string>());
//        }
//    }
//}