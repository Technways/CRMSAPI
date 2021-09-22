using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMS.Entity
{
    public class UserResponse
    {
        [Ignore]
        public bool IsCached { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public int Mobile { get; set; }
        public string Email { get; set; }
        public int PackageId { get; set; }
    }
}
