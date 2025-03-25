using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HMSContracts.Model.Test
{
    public class TestModel
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [DefaultValue(0)]
        public int Price { get; set; }
    }
}
