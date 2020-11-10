using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.AspNetCore.Mvc;
using ProductsProject.Core.DomainService;
using ProductsProject.Infrastructure.Data.Helper;


namespace ProductsWebApi.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private IUserRepository<User> userRepository;
        private IAuthenticationHelper authenticationHelper;

        public TokenController(IUserRepository<User> userRepo, IAuthenticationHelper authenticationHelp) 
        {
            userRepository = userRepo;
            authenticationHelper = authenticationHelp;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel loginmodel) 
        {
            var user = userRepository.GetAll().FirstOrDefault(u => u.Username == loginmodel.Username);

            if (user == null)
                return Unauthorized();
            if (!authenticationHelper.VerifyPasswordHash(loginmodel.Password, user.PasswordHash, user.PasswordSalt))
                return Unauthorized();

            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }
       
    }
}
