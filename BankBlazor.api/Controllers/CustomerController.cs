using BankBlazor.api.Enums;
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
        public async Task<ActionResult<PagedResult<CustomerViewDTO>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var customerEntites = await _customerService.GetAllCustomers(pageNumber, pageSize);

            var pagedResult = new PagedResult<CustomerViewDTO>
            {
                Customers = customerEntites,
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


        [HttpPost]
            
        public async Task<ActionResult> Post(CustomerCreateDTO customer)
        {
            var result = await _customerService.AddCustomer(customer);

            ResponseCode response = await _customerService.AddCustomer(customer);

            if(response == ResponseCode.Created)
            {
                return Created();
            }

            return NoContent();
        }


        [HttpPut ("{id}")]

        public async Task<ActionResult> Put(int id, CustomerUpdateDTO customer)
        {
            var result = await _customerService.UpdateCustomer(id, customer);

            return Ok();
        }

        [HttpDelete ("{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _customerService.DeleteCustomer(id);

            return Ok();

        }
       



    }

}   

