using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
