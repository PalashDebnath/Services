using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediaServices.Models
{
    public class Response
    {
        [JsonPropertyName("id")]
        public int ShowId { get; set; }

        [JsonPropertyName("name")]
        public string ShowName { get; set; }

        [JsonPropertyName("cast")]
        public List<Person> Persons { get; set; }
    }
}
