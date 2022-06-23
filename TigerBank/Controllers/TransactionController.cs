using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TigerBank.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Index()
        {
           
            return View();
        }
    }
}
