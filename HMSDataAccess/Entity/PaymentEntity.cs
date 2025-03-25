using HMSDataAccess.Interfaces;

namespace HMSDataAccess.Entity
{ 
    public class PaymentEntity : Trackable, ISoftDelete
    {
        public int Id { get; set; } 
        public int PaidAmount {  get; set; }    
        public DateOnly Date {  get; set; }
        public int ReciptNumber {  get; set; }
        public bool IsDeleted { get; set; } = false;

        public int InvoiceId {  get; set; }
        public InvoiceEntity Invoice { get; set; }
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

    }
}
