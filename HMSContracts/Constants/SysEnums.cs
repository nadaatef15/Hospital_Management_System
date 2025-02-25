namespace HMSContracts.Constants
{
    public static class SysEnums
    {
        public enum Status
        {
            complete,
            incomplete
        }

        public enum Gender
        {
            F,
            M
        }
         
        public enum model
        {
            Medicine, Payment, Specialties, Test, Invoice, Appointment
            , Prescription, Diagnoses, MedicalRecord, Patient, Doctor
            , DoctorSchedule, DoctorSpecialties, Receptionist, Pharmacist, LabTechnician,
        }

        public enum permissionType
        {
            View,
            Create,
            Edit,
            Delete,
        }
    }
}
