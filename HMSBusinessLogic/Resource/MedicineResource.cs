using System.ComponentModel.DataAnnotations;

namespace HMSBusinessLogic.Resource
{
    public class MedicineResource
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public string Type { get; set; }

        public int Amount { get; set; }
    }
}
