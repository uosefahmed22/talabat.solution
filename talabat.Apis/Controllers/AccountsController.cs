using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.Apis.Dtos.BasketDTO;
using talabat.Apis.Dtos.IdentityDTO;
using talabat.Apis.Errors;
using talabat.Apis.Extentions;
using talabat.core.Entites.identity;
using talabat.core.services;

namespace talabat.Apis.Controllers
{

    public class AccountsController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _TokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager,
                                  SignInManager<AppUser> signInManager,
                                  ITokenService TokenService,
                                  IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _TokenService = TokenService;
            _mapper = mapper;
        }
        
        [HttpPost("Login")]  // post : Api/Accounts/Login
        public async Task<ActionResult<UserDto>> login(LoginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User == null)
                return Unauthorized(new ApiResponse(401));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!Result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
                Token = await _TokenService.CreateTokenAsync(User) //"this will be atoken" 
            });
        }



        [HttpPost("register")]  // post : Api/Accounts/register
        public async Task<ActionResult<UserDto>> Register(RegisterDTO model)
        {
            if(CheckEmailExists(model.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse()
                {
                    Errors = new string[]
                    {
                        "This Email Is Already In Use !!"
                    }
                });

            var user = new AppUser
            {
                DisplayName = model.DisplayName,
                Email = model.Email, 
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.Split('@')[0] 
            };
            var Result = await _userManager.CreateAsync(user, model.Password);
            if (!Result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email, 
                Token = await _TokenService.CreateTokenAsync(user) //"this will be atoken" 
            });
        }



        [Authorize]
        [HttpGet("getcurrentuser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email =  User.FindFirstValue(ClaimTypes.Email);  
            var user = await _userManager.FindByEmailAsync(email);

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _TokenService.CreateTokenAsync(user)
            });

        }


        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindUserWithEmailAsync(User);
            var MappedAdress = _mapper.Map<Address, AddressDto>(user.Address);

            return Ok(MappedAdress);
        }


        [Authorize]
        [HttpPut("updateuseraddress")]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto UpdateAddress)
        {
            //var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindUserWithEmailAsync(User);
            var MappedAddress = _mapper.Map<AddressDto, Address>(UpdateAddress);

            user.Address = MappedAddress;

            var Result =  await _userManager.UpdateAsync(user);
            if (!Result.Succeeded) 
                return BadRequest(new ApiResponse(400));
            return Ok(UpdateAddress);
        }


        [HttpGet("checkemailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }
    }
}
