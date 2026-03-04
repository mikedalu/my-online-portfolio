using Microsoft.AspNetCore.Mvc;

namespace my_online_portfolio.Controllers
{
  public class CalculateTaxController : Controller
  {
    [HttpGet]
    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public IActionResult CalculateTax(decimal grossAnnualIncome, decimal annualRent)
    {
      // 1. Deductions
      decimal pension = grossAnnualIncome * 0.08m;
      decimal nhf = grossAnnualIncome * 0.025m; //National Housing Fund
      decimal rentRelief = Math.Min(annualRent * 0.20m, 500000m);

      // 2. Chargeable Income (Income after reliefs but before tax bands)
      decimal chargeableIncome = grossAnnualIncome - (pension + nhf + rentRelief);
      decimal originalChargeable = chargeableIncome; // Keep for display

      decimal totalTax = 0;

      // Minimum Wage Exemption Check
      if (grossAnnualIncome > 840000)
      {
        // First 800,000 @ 0%
        chargeableIncome -= 800000;

        if (chargeableIncome > 0)
        {
          decimal band = Math.Min(chargeableIncome, 2200000);
          totalTax += band * 0.15m;
          chargeableIncome -= band;
        }
        if (chargeableIncome > 0)
        {
          decimal band = Math.Min(chargeableIncome, 9000000);
          totalTax += band * 0.18m;
          chargeableIncome -= band;
        }
        if (chargeableIncome > 0)
        {
          decimal band = Math.Min(chargeableIncome, 13000000);
          totalTax += band * 0.21m;
          chargeableIncome -= band;
        }
        if (chargeableIncome > 0)
        {
          decimal band = Math.Min(chargeableIncome, 25000000);
          totalTax += band * 0.23m;
          chargeableIncome -= band;
        }
        if (chargeableIncome > 0)
        {
          totalTax += chargeableIncome * 0.25m;
        }
      }

      // Pass data to View
      ViewBag.Gross = grossAnnualIncome;
      ViewBag.Deductions = pension + nhf;
      ViewBag.RentRelief = rentRelief;
      ViewBag.Chargeable = originalChargeable;
      ViewBag.AnnualTax = totalTax;
      ViewBag.MonthlyTax = totalTax / 12;

      return View("Index");
    }

  }
}