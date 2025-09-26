using System.ComponentModel.DataAnnotations;

namespace PrescriptionApp.Models
{
    public class Prescription
    {
        public int PrescriptionId { get; set; }

        [Required(ErrorMessage = "Please enter a medication name.")]
        public string MedicationName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please select a fill status.")]
        public string FillStatus { get; set; } = "New"; // Default value

        [Required(ErrorMessage = "Please enter a cost.")]
        [Range(0.01, 10000, ErrorMessage = "Cost must be between 0.01 and 10,000.")]
        public decimal? Cost { get; set; }

        [Required(ErrorMessage = "Please enter a request date.")]
        public DateTime RequestTime { get; set; } = DateTime.Now; // Default to current time

        public string Slug =>
            MedicationName?.Replace(' ', '-').ToLower() + '-' + PrescriptionId.ToString();
    }
}