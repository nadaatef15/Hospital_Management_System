using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Specialty
{
    public class SpecialtyModel
    {
        public int? Id {  get; set; }

        [Required]
        public string Name { get; set; }
    }
}
