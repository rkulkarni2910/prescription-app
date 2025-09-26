using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrescriptionApp.Models;
using System.Diagnostics;

namespace PrescriptionApp.Controllers
{
    public class HomeController : Controller
    {
        private PrescriptionContext context { get; set; }

        public HomeController(PrescriptionContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            var prescriptions = context.Prescriptions
                .OrderBy(p => p.MedicationName).ToList();
            return View(prescriptions);
        }
    }
}