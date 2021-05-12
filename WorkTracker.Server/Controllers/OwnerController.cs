using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Resources;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using WorkTracker.Database.DTO;
using WorkTracker.Server.Exceptions;
using WorkTracker.Server.Services;
using WorkTracker.Server.Services.Contract;

namespace WorkTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OwnerController : ControllerBase
    {
        private IOwnerService _ownerService;
        private ITokenManager _tokenManager;
        private IStringLocalizer<Resource> _strLocalizer;

        public OwnerController(IOwnerService ownerService, ITokenManager mgr,
            IStringLocalizer<Resource> localizerFactory)
        {
            _ownerService = ownerService;
            _tokenManager = mgr;
            _strLocalizer = localizerFactory;
        }

        [HttpGet]
        [Route("GetUserByEmail")]
        public IActionResult GetUserByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();
            try
            {
                var res = _ownerService.RetrieveOwnerByEmail(email);
                return Ok(res);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(string name, string email, string encryptedPassword)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest(_strLocalizer["ErrorInvalidEmail"]);
            if (string.IsNullOrEmpty(name))
                return BadRequest(_strLocalizer["ErrorInvalidName"]);

            try
            {
                bool isExistingOwner = false;
                OwnerDTO owner = new OwnerDTO();
                try
                {
                    owner = _ownerService.RetrieveOwnerByEmail(email);
                    isExistingOwner = true;

                    if (owner.IsEmailVerified)
                        return BadRequest(_strLocalizer["OwnerPresent"]);

                }
                catch (Exception e)
                {

                }


                if (isExistingOwner)
                {
                    owner = _ownerService.UpdateOwner(new OwnerDTO()
                    {
                        Id = owner.Id,
                        Name = name,
                        Email = email,
                    }, encryptedPassword);
                }
                else
                {
                    owner = _ownerService.AddOwner(name, email, encryptedPassword);
                }

                return Ok(owner);
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException == null ? e : e.InnerException);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || !_ownerService.Authenticate(email, password))
            {
                return BadRequest(new WtException(_strLocalizer["AuthenticationFailure"], Constants.USERNAMEPASSWORD_WRONG));
            }

            var token = _tokenManager.GenerateJwtToken();
            return Ok(token);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("VerifyEmail")]
        public IActionResult VerifyUserEmail(string email)
        {
            try
            {
                _ownerService.VerifyEmail(email);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            return Ok(true);
        }
    }
}