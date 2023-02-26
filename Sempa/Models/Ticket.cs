using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sempa.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "*")]
        public double Price { get; set; }
        [Required(ErrorMessage = "*")]
        public string TravelTimeFrom { get; set; }
        public string TravelTimeTo { get; set; }
        [ForeignKey("flight")]
        public int FlightID { get; set; }

        //Navigation properity

        public Flight flight { get; set; }
        public List<User> Users { get; set; }
    }
}
