using BankBlazor.api.Models;
using BankBlazor.api.Services.Interfaces;
using BankBlazor_ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;

namespace BankBlazor.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var customerEntites = await _customerService.GetAllCustomers(pageNumber, pageSize);

            var customerDtos = customerEntites.Select(customerEntity => new CustomerViewDTO
            {
                Givenname = customerEntity.Givenname,
                Surname = customerEntity.Surname,
                CustomerId = customerEntity.CustomerId
            }).ToList();

            var pagedResult = new PagedResult
            {
                Customers = customerDtos,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = await _customerService.GetTotalCustomerCount()
            };

            return Ok(pagedResult);

        }

        [HttpGet("{id}")]

        public async Task<ActionResult> Get(int id)
        {
            var customer = await _customerService.GetCustomer(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);

        }

    }

}   

