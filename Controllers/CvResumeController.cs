using Microsoft.AspNetCore.Mvc;

namespace my_online_portfolio.Controllers
{
  public class CvResumeController : Controller
  {
    // GET: /CvResume/
    public IActionResult Index()
    {
      return View();
    }
  }
}
