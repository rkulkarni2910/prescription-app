using Microsoft.EntityFrameworkCore;

namespace PrescriptionApp.Models
{
    public class PrescriptionContext : DbContext
    {
        public PrescriptionContext(DbContextOptions<PrescriptionContext> options)
            : base(options)
        { }

        public DbSet<Prescription> Prescriptions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    PrescriptionId = 1,
                    MedicationName = "Atorvastatin",
                    FillStatus = "Filled",
                    Cost = 19.99m,
                    RequestTime = new DateTime(2024, 1, 15, 10, 30, 0)
                },
                new Prescription
                {
                    PrescriptionId = 2,
                    MedicationName = "Lisinopril",
                    FillStatus = "Pending",
                    Cost = 12.50m,
                    RequestTime = new DateTime(2024, 1, 16, 14, 45, 0)
                },
                new Prescription
                {
                    PrescriptionId = 3,
                    MedicationName = "Metformin",
                    FillStatus = "New",
                    Cost = 8.75m,
                    RequestTime = new DateTime(2024, 1, 17, 9, 15, 0)
                }
            );
        }
    }
}