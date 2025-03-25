using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Diagnose
{
    public class DiagnoseModel
    {
        public int? Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}
