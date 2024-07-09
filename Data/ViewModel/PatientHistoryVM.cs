using DayHospital.Models;
using DayHospital.Models.Nurse;

namespace DayHospital.Data.ViewModel
{
    public class PatientHistoryVM
    {
        public List<string>? Allergies { get; set; }
        public List<string>? Condition { get; set; }
        public List<string>? Medication { get; set; }
        public List<PrescriptionVM1>? Prescription { get; set; }
        public List<BookingAppointmentVM1>? Appointments { get; set; }
        public AdmissionVitals? Vitals { get; set; }
        public PatientUser? Patient { get; set; }
        public bool Success { get; set; }


    }
}
