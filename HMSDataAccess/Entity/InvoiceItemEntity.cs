using HMSDataAccess.Interfaces;

namespace HMSDataAccess.Entity
{
    public class InvoiceItemEntity : Trackable , ISoftDelete
    {
        public int Id { get; set; } 
        public string Name { get; set; }    
        public int UnitPrice {  get; set; }
        public int TotalPrice { get; set; } 
        public int Quentity {  get; set; }
  
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }

        public int InvoiceId {  get; set; } 
        public InvoiceEntity Invoice { get; set; }    
        
    }
}
