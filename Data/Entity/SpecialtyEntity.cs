namespace HMSDataAccess.Entity
{
    public class SpecialtyEntity 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<DoctorSpecialties> DoctorSpecialties = new List<DoctorSpecialties>();
    }
}
