using CRMS.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.BL.Interface
{
   public interface IAuthorizationBL
    {
        public Task<TokenResponse> GetUserToken(TokenRequest tokenRequest);
    }
}
