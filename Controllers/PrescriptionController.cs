using Microsoft.AspNetCore.Mvc;
using PrescriptionApp.Models;

namespace PrescriptionApp.Controllers
{
    public class PrescriptionController : Controller
    {
        private PrescriptionContext context { get; set; }

        public PrescriptionController(PrescriptionContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.FillStatuses = GetFillStatuses();
            
            // Create new prescription with current time and default status
            var prescription = new Prescription 
            { 
                RequestTime = DateTime.Now,
                FillStatus = "New" // Default as required
            };
            
            return View("Edit", prescription);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.FillStatuses = GetFillStatuses();
            var prescription = context.Prescriptions.Find(id);
            return View(prescription);
        }

        [HttpPost]
        public IActionResult Edit(Prescription prescription)
        {
            if (ModelState.IsValid)
            {
                if (prescription.PrescriptionId == 0)
                {
                    // NEW prescription - set to current system time
                    prescription.RequestTime = DateTime.Now;
                    context.Prescriptions.Add(prescription);
                }
                else
                {
                    // EXISTING prescription - update without changing RequestTime
                    var existing = context.Prescriptions.Find(prescription.PrescriptionId);
                    if (existing != null)
                    {
                        // Update only the fields that should change
                        existing.MedicationName = prescription.MedicationName;
                        existing.FillStatus = prescription.FillStatus;
                        existing.Cost = prescription.Cost;
                        // DO NOT update RequestTime for edits
                    }
                }
                context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = (prescription.PrescriptionId == 0) ? "Add" : "Edit";
                ViewBag.FillStatuses = GetFillStatuses();
                return View(prescription);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var prescription = context.Prescriptions.Find(id);
            return View(prescription);
        }

        [HttpPost]
        public IActionResult Delete(Prescription prescription)
        {
            var existingPrescription = context.Prescriptions.Find(prescription.PrescriptionId);
            if (existingPrescription != null)
            {
                context.Prescriptions.Remove(existingPrescription);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        private List<string> GetFillStatuses()
        {
            return new List<string> { "New", "Filled", "Pending" };
        }
    }
}