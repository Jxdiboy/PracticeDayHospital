using DayHospital.Models;
using DayHospital.Models.Admin;
using DayHospital.Models.Nurse;
using DayHospital.Models.Pharmacist;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DayHospital.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<PatientUser> PatientUsers { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<SurgeryBooking> SurgeryBookings { get; set; }
        public DbSet<SurgeryBookingTreatmentCode> SurgeryBookingTreatmentCodes { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Suburb> Suburbs { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<TreatmentCode> TreatmentCodes { get; set; }
        public DbSet<PrescriptionMedicine> PrescriptionMedicines { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Discharge> Discharges { get; set; }
        public DbSet<MedicineSchedule> MedicineSchedules { get; set; }
        public DbSet<MedicineActiveIngredient> MedicineActiveIngredients  { get; set; }
        public DbSet<ActiveIngredient> ActiveIngredients { get; set; }
        public DbSet<Indication> Indications { get; set; }
        public DbSet<ContraIndication> ContraIndications { get; set; }
        public DbSet<MedicationIndication> MedicationIndications { get; set; }
        public DbSet<MedicineInteraction> MedicineInteractions { get; set; }
        public DbSet<PatientAllergies> Allergies { get; set; }
        public DbSet<PatientChronicCondition> PatientChronicConditions { get; set; }
        public DbSet<PatientCurrentMedication> PatientCurrentMedications { get; set; }
        public DbSet<AdmittedPatient> AdmittedPatients { get; set; }
        public DbSet<ChronicCondition> ChronicConditions { get; set; }
        public DbSet<Dosage> Dosages { get; set; }
        public DbSet<ExtraReading> ExtraReadings { get; set; }
        public DbSet<DayHospitalRecords> DayHospitalRecords { get; set; }
        public DbSet<MedicationOrder> MedicationOrder { get; set; }
        public DbSet<OrderMed>OrderMed { get; set; }
        public DbSet<AdmissionVitals> AdmissionVitals { get; set; }
        public DbSet<Vitals> Vitals { get; set; }
        public DbSet<AdministeredMedicine> AdministeredMedicine { get; set; }
    }
}
