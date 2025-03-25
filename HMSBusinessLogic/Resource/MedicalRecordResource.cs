using HMSContracts.Model.Diagnose;
using HMSContracts.Model.Users;
using HMSDataAccess.Entity;

namespace HMSBusinessLogic.Resource
{
    public class MedicalRecordResource
    {
        public int Id { get; set; }
        public string Treatment { get; set; }

        public int Price { get; set; }

        public string Note { get; set; }

        public DoctorResource? Doctor { get; set; }

        public PatientResource? Patient { get; set; }

        public AppointmentResource? Appointment { get; set; }

        public List<DiagnoseResource>? Diagnoses { get; set; }

        public List<TestResource>? Tests { get; set; }
    }
}
