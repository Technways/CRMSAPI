using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DL.Interfaces
{
   public interface IAuthorizationDL
    {
        public Task<int> ValidateUser(string Email, string Password);
    }
}
