using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sempa.Models
{
    public class Comments
    {
        [Key]
      
        public int ID { get; set; }
        public DateTime TimeCreated { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "*")]
        public string Body { get; set; }
        public string? Stickear { get; set; } = null;
        [ForeignKey("user")]
        public int UserID { get; set; }
        [ForeignKey("post")]
        public int PostID { get; set; }
        //Navigation Properities
        public User user { get; set; }
        public Postscs post { get; set; }
    }
}
