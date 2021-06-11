namespace Pets.Controllers.Home
{
    using Microsoft.AspNetCore.Mvc;


    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}