using System.ComponentModel.DataAnnotations;

namespace Sempa.Models
{
    public class Flight
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage = "*")] 
        public string Name { get; set; }
        public DateTime ArriveTime { get; set; }
        public List<Ticket> tickets { get; set; }
    }
}
