using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMS.BL;
using CRMS.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CRMSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserDetails UserDetails { get; set; }  
        private readonly IUserDetails _userDetails;
        private readonly ILogger<IUserDetails> _logger;
        public readonly IDistributedCache _distributedCache;
        public UserController(IUserDetails userDetails, ILogger<IUserDetails> logger, IDistributedCache distributedCache)
        {
            _userDetails = userDetails;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        // GET: api/<UserController>
        [HttpGet]
        //[Authorize]
        [Route("GetUserDetails")]
        public async Task<List<UserResponse>> GetUserDetails()
        {

            return await _userDetails.GetUserDetails();
        }

        [HttpGet]
        //[Authorize]
        [Route("GetUserDetailsById/{Id}")]
        public async Task<UserResponse> GetUserDetailsById(int Id)
        {
            _logger.LogInformation("Get User Details api started");
            _logger.LogInformation("Fetching detailds for user : " + Id);
            _logger.LogInformation("user : " + Id + "details fetching from BL");

            UserResponse userResponse = new UserResponse();
            string cachedCRMSString;

            cachedCRMSString = await _distributedCache.GetStringAsync("_GetUserDetailsById_" + Id);
            if (!string.IsNullOrEmpty(cachedCRMSString))
            {
                // loaded data from the redis cache.
                userResponse = JsonSerializer.Deserialize<UserResponse>(cachedCRMSString);
                userResponse.IsCached = true;
            }
            else
            {
                // loading from code (in real-time from database)
                // then saving to the redis cache 
                UserResponse newUserResponse = await _userDetails.GetUserDetailsById(Id);
                cachedCRMSString = JsonSerializer.Serialize<UserResponse>(newUserResponse);
                await _distributedCache.SetStringAsync("_GetUserDetailsById_" + Id, cachedCRMSString);
                userResponse = newUserResponse;

            }
            return userResponse;
        }


        [HttpDelete]
        //[Authorize]
        [Route("DeleteUserDetailsById/{Id}")]
        public async Task<bool> Delete(int Id)
        {
            return await _userDetails.DeleteUserDetailsById(Id);
        }

        
        [HttpPost]
        //[Authorize]
        [Route("CreateCRMSUser")]
        public async Task<CreateUserResponse>  CreateCRMSUser(CreateUserRequest createUserRequest)
        {
           
            return await _userDetails.CreateCRMSUser(createUserRequest);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

       

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

       
    }
}
