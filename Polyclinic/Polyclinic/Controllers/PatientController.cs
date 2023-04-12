using Microsoft.AspNetCore.Mvc;

namespace Polyclinic.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
