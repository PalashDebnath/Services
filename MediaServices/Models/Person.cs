using MediaServices.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediaServices.Models
{
    public class Person
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [JsonPropertyName("name")]
        public string FullName { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Birthday { get; set; }

        [JsonIgnore]
        public ICollection<ShowPerson> ShowPersons { get; set; }
    }
}
