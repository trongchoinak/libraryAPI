﻿using libraryAPI.Models.DTO;
using libraryAPI.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace libraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        public UserController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }
        //POST:/api/Auth/Register - chức năng đăng ký user
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO) // khai báo kiểu model cho Register
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDTO.Username,
                Email = registerRequestDTO.Username
            };
            var identityResult = await _userManager.CreateAsync(identityUser,
           registerRequestDTO.Password);
            if (identityResult.Succeeded)
            {
                //add roles to this user
                if (registerRequestDTO.Roles != null && registerRequestDTO.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(identityUser,
                   registerRequestDTO.Roles);
                }
                if (identityResult.Succeeded)
                {
                    return Ok("Register Successful! Let login!");
                }
            }
            return BadRequest("Something wrong!");
        }
        //POST: /api/Auth/Login -chức năng đăng nhập User 
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] RegisterRequestDTO loginRequestDTO)
        // khai báo model cho Login 
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDTO.Username);
            if (user != null)
            {
                var checkPasswordResult = await
               _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);
            if (checkPasswordResult)
                {
                    //get roles for this user – lấy quyền của user từ database
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //create token – tạo token cho user này
                        var jwtToken = _tokenRepository.CreateJWTToken(user,
                       roles.ToList());
                        var response = new LoginResponseDTO
                        {
                            JwtToken = jwtToken
                        };

                        return Ok(response); // trả về chuỗi token
                    }
                }
            }
            return BadRequest("Username or password incorrect");
        } // end action Login
    } //
}// end class user controller 
