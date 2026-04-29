using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.api.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
