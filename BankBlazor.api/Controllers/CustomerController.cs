using BankBlazor.api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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




    }
}
