using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarBookingAPI.Model
{
    public class Car
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]
        public string Carmake { get; set; }

        [Required]
        [MaxLength(50)]
        public string CarModel { get; set; }

        [Required]
        [MaxLength(24)]
        public string CarIdentificationNumber { get; set; }

        public List<Booking> Bookings { get; set; }
    }

    public class Booking
    {
        public int ID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CarId { get; set; }

        public Car Car { get; set; }
    }

}
