﻿using HITSBackEnd.Dto;
using HITSBackEnd.Services.Account.IRepository;
using HITSBackEnd.Swagger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HITSBackEnd.Controllers
{
    [Route("api/account")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var RegistrationLoginResponceDTO = await _userRepository.Login(model);
            if (string.IsNullOrEmpty(RegistrationLoginResponceDTO.Token))
            {
                throw new Exception(ErrorCreator.CreateError("Неверный формат телефона"));
            }
            return Ok(RegistrationLoginResponceDTO);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            var RegistrationLoginResponceDTO = await _userRepository.Register(model);
            return Ok(RegistrationLoginResponceDTO);
        }
        [HttpGet("profile")]
        [Authorize]
        public IActionResult GetProfile()
        {
            var ProfileResponseDTO = _userRepository.Profile(User.Identity.Name);
            return Ok(ProfileResponseDTO);
        }
        
    }
}
