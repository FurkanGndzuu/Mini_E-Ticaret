using ETicareAPI.Domain.Entities.IdentityEntities;
using ETicaretAPI.Application.Abstractions.Authentication;
using ETicaretAPI.Application.Abstractions.Tokens;
using ETicaretAPI.Application.Abstractions.User;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using ETicaretAPI.Application.Features.Commands.Users.LoginUser;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace ETicaretAPI.Presistance.Services
{
    public class AuthService : IAuth
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        readonly IConfiguration _config;
        readonly HttpClient _httpClient;
        readonly IUserServices _userService;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IConfiguration config, HttpClient httpClient, IUserServices userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _config = config;
            _httpClient = httpClient;
            _userService = userService;
        }

        public async Task<Token> FacebookLoginFunc(string authToken)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync(_config["FacebookLogin:accessTokenResponse"]);

            FacebookAccessTokenResponse? response = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);

            string userAccess = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={response.access_token}");
            FacebookAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookAccessTokenValidation>(userAccess);

            if (validation.data.IsValid)
            {
                string userInfoName = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoName);

                UserLoginInfo userLoginInfo = new("FACEBOOK", validation.data.UserId, "FACEBOOK");

                AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

                bool result = user != null;
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userInfo.Email);
                    if (user == null)
                    {
                        user = new() { Id = Guid.NewGuid().ToString(), Email = userInfo.Email, UserName = userInfo.Email, nameSurname = userInfo.Name };
                        IdentityResult createResult = await _userManager.CreateAsync(user);
                        result = createResult.Succeeded;
                    }
                }

                if (result)
                    await _userManager.AddLoginAsync(user, userLoginInfo);


                Token token = _tokenHandler.CreateAccessToken(1000 , user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, 1200);

                return token;
            }

            else
                throw new Exception("Invalid external authentication.");
        }

        public async Task<Token> GoogleLoginFunc(string IdToken)
        {
            ValidationSettings? settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                { _config["ExternalLogin:Google-Client-Id"] }
            };

            Payload payload = await GoogleJsonWebSignature.ValidateAsync(IdToken, settings);
            UserLoginInfo userLoginInfo = new("GOOGLE", payload.Subject, "GOOGLE");

            AppUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new() { Id = Guid.NewGuid().ToString(), Email = payload.Email, UserName = payload.Email, nameSurname = payload.Name };
                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }

            if (result)
                await _userManager.AddLoginAsync(user, userLoginInfo);
            else
                throw new Exception("Invalid external authentication.");

            Token token = _tokenHandler.CreateAccessToken(1000 ,user);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, 1200);
            return token;
        }

        public async Task<Token> LoginFunc(string userOrEmailName, string password)
        {
            AppUser user = await _userManager.FindByNameAsync(userOrEmailName);
            if (user == null)
                await _userManager.FindByEmailAsync(userOrEmailName);
            if (user == null)
                throw new userNullException();

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(1000 , user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, 1200);

                return token;
            }
            throw new Exception("Exception");

        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.refreshToken == refreshToken);

            if (user != null && user?.refreshTokenEndDate >= DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(1000, user);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, 1200);
                return token;
            }
            else throw new Exception();
        }
    }
}
