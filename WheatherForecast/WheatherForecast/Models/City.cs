using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WheatherForecast.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Coordinates Coord { get; set; }
        public string Country { get; set; }
        public int Population { get; set; }
    }
}