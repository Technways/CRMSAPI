using CRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DL
{
   public interface IUserDetailsDL
    {
        public Task<List<UserResponse>> GetUserDetails();
        public Task<UserResponse> GetUserDetailsByIdAsync(int Id);

        public Task<bool> DeleteUserDetailsByIdAsync(int Id);

        public Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest);
    }
}
