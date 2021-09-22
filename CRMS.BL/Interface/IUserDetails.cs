using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CRMS.Entity;

namespace CRMS.BL
{
    public interface IUserDetails
    {
        public Task<List<UserResponse>> GetUserDetails();
        public  Task<UserResponse> GetUserDetailsById(int Id);
        public Task<bool> DeleteUserDetailsById(int Id);
        public Task<CreateUserResponse> CreateCRMSUser(CreateUserRequest createUserRequest);
    }
}
