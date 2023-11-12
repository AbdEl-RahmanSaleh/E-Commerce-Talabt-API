using Core.IdentityEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Services.TokenService;
using Services.Services.UserService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.UserService
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;

        public UserServices(UserManager<AppUser> userManager,ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        #region Register Service
        public async Task<UserDto> Register(RegisterDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user != null)
                return null;

            var appUser = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email.Split('@')[0]
            };

            var result = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (!result.Succeeded)
                return null;
            return new UserDto
            {
                DisplayName = appUser.DisplayName,
                Email = appUser.Email,
                Token = _tokenService.CreateToken(appUser)
            };
        }
        #endregion

        #region LogIn Service
        public async Task<UserDto> Login(LogInDto logInDto)
        {
            var user = await _userManager.FindByEmailAsync(logInDto.Email);
            if (user == null)
                return null;
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, logInDto.Password,false);
            if (!result.Succeeded)
                return null;

            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
        #endregion




    }
}
