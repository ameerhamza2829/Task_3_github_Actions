using CMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize(SD.Role_Admin)]
    public class AdminPanel : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
