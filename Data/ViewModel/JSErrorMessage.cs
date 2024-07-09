namespace DayHospital.Data.ViewModel
{
    public class JSMessage
    {
        public string Message { get; set; }
        public string Message1 { get; set; }
        public bool Success { get; set; }
        public bool Status { get; set; }
        public string Reason { get; set; }
        public string Patient { get; set; }

    }
    public class DischargeVM
    {
        public string id { get; set; }
        public string note { get; set; }
        public string time { get; set; }
    }
}
