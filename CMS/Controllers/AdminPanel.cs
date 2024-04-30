using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    public class AdminPanel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
