using System;
using System.Collections.Generic;
using System.Text;

namespace CRMS.Entity
{
   public class CreateUserRequest
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public int PackageId { get; set; }

    }
}
