namespace DayHospital.Data.ViewModel
{
    public class SurgeonDashboard
    {
        public IEnumerable<DischargeSD>? Discharges { get; set; }
        public IEnumerable<PrescriptionSD>? Prescriptions { get; set; }
        public IEnumerable<AdmittedPatVMSD>? AdmittedPats  { get; set; }
        public AppointmentStats? Stats  { get; set; }
    }
    public class AppointmentStats
    {
        public int Total { get; set; }
        public int Admitted { get; set; }
        public int LateArrivals { get; set; }
        public int Discharged { get; set; }
    }
    public class PrescriptionSD
    {
        public string Phrama { get; set; }
        public string Patient { get; set; }
        public string PatientId { get; set; }
        public int PresID { get; set; }
        public string Priority { get; set; }
        public string Date { get; set; }
        public string? Reason { get; set; }
        public string? FullReason { get; set; }
    }

    public class DischargeSD
    {
        public int BookingId { get; set; }
        public string PatId { get; set; }
        public string Patient { get; set; }
        public string Gender { get; set; }
        public string Ward { get; set; }
        public string Bed { get; set; }
    }
    public class AdmittedPatVMSD
    {
        public int BookingId { get; set; }
        public string PatId { get; set; }
        public string Patient { get; set; }
        public string Gender { get; set; }
        public string Ward { get; set; }
        public string Bed { get; set; }
        public string Theater { get; set; }
        public string DateWithSession { get; set; }
        public string NurseName { get; set; } = "John Dou1";
    }
    public class BookingList
    {
        public string Time { get; set; }
        public int BookingId { get; set; }
        public string PatId { get; set; }
        public string Patient { get; set; }
        public string Theater { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        public bool IsConfirmed { get; set; }
        
    }
    public class PrescriptionList
    {
        public string Priority { get; set; }
        public int PresId { get; set; }
        public string PatId { get; set; }
        public string Patient { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
        
    }

}
