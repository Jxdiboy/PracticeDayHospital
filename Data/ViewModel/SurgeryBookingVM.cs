namespace DayHospital.Data.ViewModel
{
    public class SurgeryBookingVM
    {
        public int Id { get; set; }
        public string PatientIDNumber { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public int TheaterId { get; set; }
        public int SurgeonId { get; set; }
        public DateTime Date { get; set; }
        public string TreatmentCode { get; set; }
        public string Time { get; set; }
    }
}
