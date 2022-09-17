using System.Net;
using BanksDemo.Shared.Events.Abstract;
using BanksDemo.Shared.Events.Concrete;
using BanksDemo.Shared.Helpers;
using BanksDemo.User.BusinessRules;
using BanksDemo.User.Constants;
using BanksDemo.User.DTOs;
using BanksDemo.User.Repositories.Abstract;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace BanksDemo.User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly UserTransferRules _userTransferRules;
        private readonly IPublishEndpoint _publishEndpoint;
        public UsersController(IUserRepository userRepository, UserTransferRules userTransferRules, IPublishEndpoint publishEndpoint)
        {
            _userRepository = userRepository;
            _userTransferRules = userTransferRules;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("getList")]
        [ProducesResponseType(typeof(IEnumerable<UserForListDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetList()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("getById/{userId}")]
        [ProducesResponseType(typeof(UserForListDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(UserForRegisterDto userForRegisterDto)
        {
            await _userRepository.CreateAsync(userForRegisterDto);
            return NoContent();
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferRequestDto transferRequestDto)
        {
            var ruleResult =
                BusinessRulesHelper.CheckRules
                (
                    await _userTransferRules.IsUserFoundAndActiveByUserId(transferRequestDto.CurrentUserId)
            , await _userTransferRules.IsUserFoundAndActiveByUserId(transferRequestDto.ToUserId),
                    await _userTransferRules.UserWalletCheck(transferRequestDto.CurrentUserId, transferRequestDto.Amount, true),
                    await _userTransferRules.UserWalletCheck(transferRequestDto.ToUserId, transferRequestDto.Amount, false)
                );
            if (!string.IsNullOrEmpty(ruleResult))
                return BadRequest(new { Success = false, Message = ruleResult });
            var currentUser = await _userRepository.GetByIdAsync(transferRequestDto.CurrentUserId);
            var toUser = await _userRepository.GetByIdAsync(transferRequestDto.ToUserId);
            await _publishEndpoint.Publish(new TransferEvent(currentUser!.FirstName, currentUser.LastName, currentUser.Id, toUser!.FirstName, transferRequestDto.Amount,
                toUser.LastName, transferRequestDto.ToUserId));
            return Ok(new { Success = false, Message = ApiResponseMessages.TransferRequestSuccess });
        }

    }
}
