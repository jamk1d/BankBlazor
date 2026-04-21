using Microsoft.EntityFrameworkCore;
using BankBlazor.api.Enums;
using BankBlazor.api.Services.Interfaces;
using BankBlazor.api.Data;
using BankBlazor.api.Models;
using BankBlazor_ClassLibrary;
using BankBlazor_ClassLibrary.DTOs;


namespace BankBlazor.api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankBlazorContext _dbContext;

        public CustomerService(BankBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Customer>> GetAllCustomers(int pageNumber, int pageSize)
        {
            // Beräknar vilken produktindex som ska startas från för den aktuella sidan
            var products = await _dbContext.Customers
                .Skip((pageNumber - 1) * pageSize)  // Hoppar över tidigare sidor
                .Take(pageSize)  // Tar endast så många produkter som behövs för sidan
                .ToListAsync();

            return products;

        }

        public async Task<List<Customer>> GetAllCustomers()
        {
            var customers = await _dbContext.Customers.ToListAsync();

            return customers;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            var Customer = await _dbContext.Customers.Include(c => c.Dispositions).ThenInclude(C => C.Account).FirstOrDefaultAsync(c => c.CustomerId == id);

            var dto = new CustomerDto
            {
                Givenname = Customer.Givenname,
                Surname = Customer.Surname,
                Streetaddress = Customer.Streetaddress,
                City = Customer.City,
                Country = Customer.Country,
                Telephonenumber = Customer.Telephonenumber,
                Emailaddress = Customer.Emailaddress,
                Birthday = Customer.Birthday


            };

            return Customer;
        }

        public async Task<ResponseCode> AddCustomer(Customer customer)
        {
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            return ResponseCode.Created;
        }

        public async Task<ResponseCode> UpdateCustomer(Customer customer)
        {
            return ResponseCode.Accepted;
        }

        public async Task<ResponseCode> DeleteCustomer(int id)
        {
            return ResponseCode.Accepted;
        }

            

    }
}