using BankBlazor.api.Enums;
using BankBlazor_ClassLibrary.DTOs;

namespace BankBlazor.api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountviewDTO> GetAccountBalance(int accountId);

        Task<ResponseCode> Deposit(int accountId, decimal ammount);

        Task<ResponseCode> Withdraw(int accountId, decimal amount);

        Task<ResponseCode> Transfer(int fromAccountId, int toAccountId, decimal ammount);
    }
}
