using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sempa.Models
{
    public class Postscs
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "*")]
        public string Title { get; set; }
        [Required(ErrorMessage = "*")]
        public string Image { get; set; }
        [Required(ErrorMessage = "*")]
        public string Body { get; set; }
        public int PostLike { get; set; } = 0;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [ForeignKey("user")]
        public int? UserId { get; set; }
        //Navigation Properity
        public User user { get; set; }
        public List<Comments> comments { get; set; }


    }
}
