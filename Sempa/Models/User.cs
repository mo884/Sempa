using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Sempa.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [StringLength(20, ErrorMessage = "The Max Length 20 !"), Required]
        public string Name { get; set; }
       
        public string Password { get; set; }
        [NotMapped]
        [Compare("Password", ErrorMessage = "The Password Not Correct !")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "*")]
        public string gender { get; set; }

        public string? School { get; set; }

        public string? Image { get; set; }
        //Navigation Properity
        public List<Postscs> posts { get; set; }
        public List<Comments> comments { get; set; }
        public List<Ticket> tickets { get; set; }


    }
}
