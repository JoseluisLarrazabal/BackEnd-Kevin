namespace CB_Backend_FAB.Models
{
    public class RecordTreatments
    {
        public int RecordTreatmentsID { get; set; }
        public DateTime AttentionDate { get; set; }
        public string Diagnosis { get; set; }
        public string Treatment { get; set; }
        public Person? Person { get; set; }
        public byte Status { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public short UserID { get; set; }

        public RecordTreatments() { }

        public RecordTreatments(int recordTreatmentsID, DateTime attentionDate, string diagnosis, string treatment, Person? person)
        {
            RecordTreatmentsID = recordTreatmentsID;
            AttentionDate = attentionDate;
            Diagnosis = diagnosis;
            Treatment = treatment;
            Person = person;
        }
    }
}
