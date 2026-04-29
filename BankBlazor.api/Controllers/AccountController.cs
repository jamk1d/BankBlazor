using Microsoft.AspNetCore.Mvc;
using BankBlazor.api.Services.Interfaces;
using BankBlazor_ClassLibrary.DTOs;
using BankBlazor.api.Enums;

namespace BankBlazor.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService AccountService)
        {
            _accountService = AccountService;
        }

        [HttpGet("{accountId}/balance")]

        public async Task<ActionResult> GetBalance(int accountId)
        {
            var accountBalance = await _accountService.GetAccountBalance(accountId);

            if(accountBalance == null)
            {
                return NotFound();
            }

            return Ok(accountBalance);
        }


        [HttpPost("{accountId}/deposit")]

        public async Task<ActionResult> Deposit(int accountId, [FromBody] AmountDTO dto)
        {
            var accountDeposit = await _accountService.Deposit(accountId, dto.Amount);

            if(accountDeposit == ResponseCode.NotFound)
            {
                return NotFound();
            }

            return Ok(accountDeposit);
        }


        [HttpPost("{accountId}/withdraw")]

        public async Task<ActionResult> Withdraw(int accountId, [FromBody] AmountDTO dto)
        {
            var accountWithDraw = await _accountService.Withdraw(accountId, dto.Amount);

            if(accountWithDraw == ResponseCode.NotFound)
            {
                return NotFound();
            }

            return Ok(accountWithDraw);
        }

        [HttpPost("transfer")]

        public async Task<ActionResult> Transfer([FromBody] TransferDTO transferDTO)
        {
            var account = await _accountService.Transfer(transferDTO.FromAccountId, transferDTO.ToAccountId, transferDTO.Amount);

            if (account == ResponseCode.BadRequest)
            {
                return BadRequest();
            }

            return Ok(account);
            
        }


    }
}
