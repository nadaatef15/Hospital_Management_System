using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Medicine
{
    public class MedicineModel
    {
        public int ? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue , ErrorMessage ="Enter positive price")]
        
        public int Price { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Minimum amount is 0")]
        public int Amount { get; set; }
      

    }
}
