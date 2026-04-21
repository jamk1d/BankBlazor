using BankBlazor.api.Models;
using BankBlazor.api.Enums;


namespace BankBlazor.api.Services.Interfaces

{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers(int pageNumber, int pageSize);
        Task<Customer> GetCustomer(int id);
        Task<ResponseCode> AddCustomer(Customer customer);
        Task<ResponseCode> UpdateCustomer(Customer customer);
        Task<ResponseCode> DeleteCustomer(int id);


    }
}
