using System.ComponentModel.DataAnnotations;
using static HMSContracts.Constants.SysEnums;
using Microsoft.AspNetCore.Http;

namespace HMSContracts.Model.Identity
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [Range(maximum: 100, minimum: 1)]
        public int Age { get; set; }

        [Required]
        [RegularExpression("^[FM]$", ErrorMessage = "Gender must be 'F' or 'M'.")]
        public char Gender { get; set; }

        [Required]
       // [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n", ErrorMessage = "Email does not follow the pattern")]
        public string Email { get; set; }

        [Required]
       // [RegularExpression("^01[0-5]\\d{8}$\r\n")]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }

        public IFormFile? Image { get; set; }

    }
}
