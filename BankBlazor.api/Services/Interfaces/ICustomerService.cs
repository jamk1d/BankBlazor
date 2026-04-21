using BankBlazor.api.Models;
using BankBlazor.api.Enums;
using BankBlazor_ClassLibrary.DTOs;


namespace BankBlazor.api.Services.Interfaces

{
    public interface ICustomerService
    {
        Task<List<CustomerViewDTO>> GetAllCustomers(int pageNumber, int pageSize);
        Task<CustomerDTO> GetCustomer(int id);
        Task<ResponseCode> AddCustomer(Customer customer);
        Task<ResponseCode> UpdateCustomer(Customer customer);
        Task<ResponseCode> DeleteCustomer(int id);
        Task<int> GetTotalCustomerCount();


    }
}
