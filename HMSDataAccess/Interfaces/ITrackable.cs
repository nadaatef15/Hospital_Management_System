namespace HMSDataAccess.Interfaces
{
    public interface ITrackable
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
