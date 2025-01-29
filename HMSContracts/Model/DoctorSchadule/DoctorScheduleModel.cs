using HMSContracts.CustomValidation;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.DoctorSchadule
{ 
    public interface ITimeRange
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
    public class DoctorScheduleModel : ITimeRange
    {
        public int? Id { get; set; }

        [Required]
        [DateNotInThePast]
        public DateOnly Date { get; set; }

        [Required]
        [Description("the format is HH:MM:SS")]
        [SwaggerSchema(Format = "time", Description = "Time in HH:mm:ss format")]
        public TimeOnly StartTime { get; set; }

        [Required]
        [Description("the format is HH:MM:SS")]
        [SwaggerSchema(Format = "time", Description = "Time in HH:mm:ss format")]
        [EndTimeAfterStartTime]
        public TimeOnly EndTime { get; set; }

        [Required]
        public string DoctorId { get; set; }

    }
}
