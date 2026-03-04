using Microsoft.AspNetCore.Mvc;

namespace my_online_portfolio.Controllers
{
  public class AboutMeController : Controller
  {
    // GET: /AboutMe/
    public IActionResult Index()
    {
      return View();
    }
  }
}
