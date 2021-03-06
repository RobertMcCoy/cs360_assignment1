using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using ChessClubManagement.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http;
using System.Text;
using ChessClubManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ChessClubManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<OpenIdConnectOptions> _options;
        private readonly HomeRepository _repository;

        public HomeController(IOptions<OpenIdConnectOptions> options, ChessClubContext context)
        {
            _options = options;
            _repository = new HomeRepository(context);
        }

        public IActionResult Index() {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            LockContext lockContext = HttpContext.GenerateLockContext(_options.Value, returnUrl);

            return View(lockContext);
        }

        [Authorize]
        public IActionResult Profile()
        {
            return View(new UserProfileViewModel()
            {
                Name = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
                PhoneNumber = JObject.Parse(User.Claims.FirstOrDefault(c => c.Type == "user_metadata").Value)["phonenumber"].ToString(),
                UserId = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value.Substring(6)
            });
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            // Sign the user out of the authentication middleware (i.e. it will clear the Auth cookie)
            HttpContext.Authentication.SignOutAsync("Auth0");
            HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect the user to the home page after signing out
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View();
        }
        
        [Authorize]
        public IActionResult UpdateUser(UserProfileViewModel viewModel)
        {
            _repository.UpdateProfile(viewModel);
            return RedirectToAction("Profile");
        }
    }
}
