using HMSDataAccess.Interfaces;

namespace HMSDataAccess.Entity
{
    public class Trackable : ITrackable
    {
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
