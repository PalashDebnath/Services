using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediaServices.Models
{
    public class Cast
    {
        [Required]
        [JsonPropertyName("person")]
        public Person Person { get; set; }
    }
}
