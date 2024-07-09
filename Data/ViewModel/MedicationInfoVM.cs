namespace DayHospital.Data.ViewModel
{
    public class MedicationInfoVM
    {
        public string Name { get; set; }
        public string Dosage { get; set; }
        public int Quantity { get; set; }
        public string Schedulelevel { get; set; }
        public List<string> ActiveIn { get; set; }
        public List<string> Indication { get; set; }
        public List<string> ContraInd { get; set; }
        public List<string> Interaction { get; set; }
    }
}
