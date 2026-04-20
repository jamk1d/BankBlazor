using BankBlazor.api.Models;





namespace BankBlazor.api.Services.Interfaces

{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomers(int pageNumber, int pageSize);
        Task<Customer> GetCustomer(int id);


    }
}
