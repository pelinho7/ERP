using AutoMapper;
using Dictionaries.Enums;
using EmailService;
using Identity.DBAccess.Data;
using Identity.DBAccess.Entities;
using Identity.DBAccess.Interfaces;
using Identity.DTOs;
using Identity.Extensions;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Transactions;
using System.Xml;

namespace Identity.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UsersController> logger;

        public UsersController(IMapper mapper
            , ILogger<UsersController> logger, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.userRepository = userRepository;
        }



        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<UserBasicDto>>> GetUsers([FromQuery]GetUsersFilter filter)
        {
            var users = await userRepository.GetUsersAsync(filter);
            var userDtos = users.Select(x => new UserBasicDto(x.Id, x.UserName, x.Email)).ToList();

            return userDtos;
        }
    }
}
