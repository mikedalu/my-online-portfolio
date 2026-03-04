using Microsoft.AspNetCore.Mvc;

namespace my_online_portfolio.Controllers
{
  public class ContactController : Controller
  {
    // GET: /contact/
    public IActionResult Index()
    {
      return View();
    }
  }
}
