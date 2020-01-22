using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarBookingAPI.Model
{
    public class Car
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("carmake")]
        public string Carmake { get; set; }


        [JsonPropertyName("carModel")]
        public string CarModel { get; set; }


        [JsonPropertyName("carIdentificationNumber")]
        public string CarIdentificationNumber { get; set; }

    }

    public class Booking
    {

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("carId")]
        public int CarId { get; set; }

    }

}
