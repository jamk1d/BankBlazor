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

        public async Task<ActionResult> Deposit(int accountId, decimal ammount)
        {
            var deposit = await _accountService.Deposit(accountId, ammount);

            return Ok(deposit);
        }


        [HttpPost("{accountId}/withdraw")]

        public async Task<ActionResult> Withdraw(int accountId, decimal ammount)
        {
            var withdraw = await _accountService.Withdraw(accountId, ammount);

            return Ok(withdraw);
        }

        [HttpPost("transfer")]

        public async Task<ActionResult> Transfer(int fromAccountId, int toAccountId, decimal ammount)
        {
            var account = await _accountService.Transfer(fromAccountId, toAccountId, ammount);

            return Ok(account);
            
        }


    }
}
