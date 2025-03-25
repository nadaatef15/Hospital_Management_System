using HMSDataAccess.Interfaces;
using static HMSContracts.Constants.SysEnums;
namespace HMSDataAccess.Entity
{
    public class InvoiceEntity : Trackable, ISoftDelete
    {
        public int Id { get; set; } 
        public DateOnly Date {  get; set; } 
        public int Total { get; set; }  
        public Status Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        public string PatientId {  get; set; }  
        public PatientEntity Patient { get; set; }

        public int AppointmentId {  get; set; } 
        public AppointmentEntity Appointment { get; set; }

    }
}
