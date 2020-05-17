using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MediaServices.Common
{
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            value = string.IsNullOrEmpty(value) ? "1900-01-01" : value;
            return DateTime.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            var formattedValue = value.ToString("yyyy-MM-dd");
            if (formattedValue == "1900-01-01")
                formattedValue = string.Empty;
            writer.WriteStringValue(formattedValue);
        }
    }
}
