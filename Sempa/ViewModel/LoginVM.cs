using System.ComponentModel.DataAnnotations;

namespace Sempa.ViewModel
{
    public class LoginVM
    {

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        //[RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password Must be have ^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$")]
        public string Passward { get; set; }
        public string Message { get; set; } = "";
    }
}
