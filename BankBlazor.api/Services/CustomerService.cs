using Microsoft.EntityFrameworkCore;
using BankBlazor.api.Enums;
using BankBlazor.api.Services.Interfaces;
using BankBlazor.api.Data;
using BankBlazor.api.Models;
using BankBlazor_ClassLibrary;
using BankBlazor_ClassLibrary.DTOs;
using Azure.Identity;
using Microsoft.AspNetCore.Http.HttpResults;


namespace BankBlazor.api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankBlazorContext _dbContext;

        public CustomerService(BankBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CustomerViewDTO>> GetAllCustomers(int pageNumber, int pageSize)
        {
            // Beräknar vilken produktindex som ska startas från för den aktuella sidan
            var customers = await _dbContext.Customers
                .Skip((pageNumber - 1) * pageSize)  // Hoppar över tidigare sidor
                .Take(pageSize)  // Tar endast så många produkter som behövs för sidan
                .ToListAsync();

            return customers.Select(c => new CustomerViewDTO
            {
                Givenname = c.Givenname,
                Surname = c.Surname,
                CustomerId = c.CustomerId
            }).ToList();

        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = await _dbContext.Customers.ToListAsync();

            return customers;
        }

        public async Task<CustomerDTO> GetCustomer(int id)
        {
            var Customer = await _dbContext.Customers.Include(c => c.Dispositions).ThenInclude(C => C.Account).FirstOrDefaultAsync(c => c.CustomerId == id);

            var dto = new CustomerDTO
            {
                Gender = Customer.Gender,
                Givenname = Customer.Givenname,
                Surname = Customer.Surname,
                Streetaddress = Customer.Streetaddress,
                City = Customer.City,
                Country = Customer.Country,
                Telephonenumber = Customer.Telephonenumber,
                Emailaddress = Customer.Emailaddress,
                Birthday = Customer.Birthday,
                Accounts = Customer.Dispositions.Select(d => new AccountDTO
                {
                    AccountId = d.Account.AccountId,
                    Balance = d.Account.Balance
                }).ToList()
                


            };

            return dto;
        }

        public async Task<ResponseCode> AddCustomer(CustomerCreateDTO customer)
        {
            var newCustomer = new Customer
            {
                Givenname = customer.Givenname,
                Surname = customer.Surname,
                Streetaddress = customer.Streetaddress,
                City = customer.City,
                Country = customer.Country,
                CountryCode = customer.CountryCode,
                Telephonenumber = customer.Telephonenumber,
                Emailaddress = customer.Emailaddress,
                Birthday = customer.Birthday,
                Zipcode = customer.Zipcode,
                Gender = customer.Gender,
                Telephonecountrycode = customer.Telephonecountrycode
            };

            var newAccount = new Account
            {
                Frequency = "Monthly",
                Created = DateOnly.FromDateTime(DateTime.Now),
                Balance = 0,
            };

            var newDispostion = new Disposition
            {
                Type = "Owner",
                Customer = newCustomer,
                Account = newAccount
            };

            await _dbContext.Customers.AddAsync(newCustomer);
            await _dbContext.Accounts.AddAsync(newAccount);
            await _dbContext.Dispositions.AddAsync(newDispostion);
            await _dbContext.SaveChangesAsync();

            return ResponseCode.Created;
        }

        public async Task<ResponseCode> UpdateCustomer(int id, CustomerUpdateDTO customer)
        {
            var updateCustomer = await _dbContext.Customers.FirstOrDefaultAsync(C => C.CustomerId == id);

            if (updateCustomer == null)
            {
                return ResponseCode.NotFound;
            }

            updateCustomer.Givenname = customer.Givenname;
            updateCustomer.Surname = customer.Surname;
            updateCustomer.Telephonenumber = customer.Telephonenumber;
            updateCustomer.Gender = customer.Gender;
            updateCustomer.Streetaddress = customer.Streetaddress;
            updateCustomer.City = customer.City;
            updateCustomer.Emailaddress = customer.Emailaddress;
            updateCustomer.Birthday = customer.Birthday;
            updateCustomer.Zipcode = customer.Zipcode;
            updateCustomer.Telephonecountrycode = customer.Telephonecountrycode;

            await _dbContext.SaveChangesAsync();
            return ResponseCode.Accepted;
        }

        public async Task<ResponseCode> DeleteCustomer(int id)
        {
            var deleteCustomer = await _dbContext.Customers.Include(C => C.Dispositions)
                .ThenInclude(C => C.Account).FirstOrDefaultAsync(C => C.CustomerId == id);

            if(deleteCustomer == null)
            {
                return ResponseCode.NotFound;
            }


            var accounts = deleteCustomer.Dispositions.Select(C => C.Account).ToList();

            _dbContext.RemoveRange(deleteCustomer.Dispositions);
            _dbContext.RemoveRange(accounts);
            _dbContext.Remove(deleteCustomer);
            await _dbContext.SaveChangesAsync();

            return ResponseCode.Accepted;
        }

        public async Task<int> GetTotalCustomerCount()
        {
            return await _dbContext.Customers.CountAsync();
        }
            

    }
}