﻿using Management.Services.AuthAPI.Model.DTO;
using Management.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace Management.Services.AuthAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthApiController(IAuthService authService)
        {
            _authService = authService;
            _response = new();
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterationRequestDto requestDto)
        {
            var errorMessage = await _authService.Register(requestDto);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var loginResponse = await _authService.Login(loginRequestDto);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or Password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;

            return Ok(_response);
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterationRequestDto requestDto)
        {
            var isRoleAssigned = await _authService.AssignRole(requestDto.Email, requestDto.Role.ToUpper());
            if (!isRoleAssigned)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }

            return Ok(_response);
        }
    }
}
