using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
