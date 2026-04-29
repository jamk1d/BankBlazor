using Azure.Identity;
using BankBlazor.api.Data;
using BankBlazor.api.Enums;
using BankBlazor.api.Models;
using BankBlazor.api.Services.Interfaces;
using BankBlazor_ClassLibrary;
using BankBlazor_ClassLibrary.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace BankBlazor.api.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankBlazorContext _dbContext;

        public AccountService(BankBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<AccountviewDTO> GetAccountBalance(int accountId)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

            if(account == null)
            {
                return null;
            }
            

            var dto = new AccountviewDTO
            {
                Balance =  account.Balance
            };

            return dto;
        }
        

        public async Task<ResponseCode> Deposit(int accountId, decimal ammount)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

            if(account == null)
            {
                return ResponseCode.NotFound;
            }

            account.Balance += ammount;

            var newTransaction = new Transaction
            {
                AccountId = account.AccountId,
                Type = "Debit",
                Amount = ammount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Operation = "Deposit",
                Balance = account.Balance,
            };

            await _dbContext.AddAsync(newTransaction);
            await _dbContext.SaveChangesAsync();
            return ResponseCode.Success;
        }

        public async Task<ResponseCode> Withdraw(int accountId, decimal ammount)
        {
            var account = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

            if (account == null)
            {
                return ResponseCode.NotFound;
            }

            account.Balance -= ammount;

            var newTransaction = new Transaction
            {
                AccountId = account.AccountId,
                Type = "Debit",
                Amount = ammount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Operation = "Withdrawal in Cash",
                Balance = account.Balance,
            };

            await _dbContext.AddAsync(newTransaction);
            await _dbContext.SaveChangesAsync();

            return ResponseCode.Success;

        }


        public async Task<ResponseCode> Transfer(int fromAccountId, int toAccountId, decimal ammount)
        {
            var account1 = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == fromAccountId);

            if (account1 == null)
            {
                return ResponseCode.NotFound;
            }

            var account2 = await _dbContext.Accounts.FirstOrDefaultAsync(a => a.AccountId == toAccountId);

            if (account2 == null)
            {
                return ResponseCode.NotFound;
            }

            if (account1.Balance < ammount)
            {
                return ResponseCode.BadRequest;
            }

            account1.Balance -= ammount;
            account2.Balance += ammount;

            var newTransaction = new Transaction
            {
                AccountId = account1.AccountId,
                Type = "Debit",
                Amount = ammount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Operation = "Transfer To Another Bank",
                Balance = account1.Balance
            };

            var newTransaction2 = new Transaction
            {
                AccountId = account2.AccountId,
                Type = "Debit",
                Amount = ammount,
                Date = DateOnly.FromDateTime(DateTime.Now),
                Operation = "Transfer from Another Bank",
                Balance = account2.Balance
            };

            await _dbContext.AddRangeAsync(newTransaction, newTransaction2);
            await _dbContext.SaveChangesAsync();

            return ResponseCode.Success;

            
        }







        
    }
}
