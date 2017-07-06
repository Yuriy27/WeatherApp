using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WheatherForecast.Models
{
    public class Coordinates
    {
        [JsonProperty(PropertyName = "lon")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "lat")]
        public double Latitude { get; set; }
    }
}