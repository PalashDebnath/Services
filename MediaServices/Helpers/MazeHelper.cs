using MediaServices.Common;
using MediaServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaServices.Helpers
{
    public class MazeHelper
    {
        private const string BASE_URI = "http://api.tvmaze.com/";
        private const string SHOW_ENDPOINT = "shows";
        private const string CAST_ENDPOINT = "shows/{0}/cast";

        public static async Task<IEnumerable<Show>> GetShows()
        {
            var shows = new List<Show>();

            string restURL = BASE_URI + SHOW_ENDPOINT;

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(restURL);
                var json = await response.Content.ReadAsStringAsync();
                shows = JsonSerializer.Deserialize<List<Show>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            return shows;
        }

        public static async Task<IEnumerable<Cast>> GetCasts(int showId)
        {
            var casts = new List<Cast>();

            string restURL = BASE_URI + String.Format(CAST_ENDPOINT, showId);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(restURL);
                var json = await response.Content.ReadAsStringAsync();
                casts = JsonSerializer.Deserialize<List<Cast>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, Converters = { new CustomDateTimeConverter() } }) ;
            }

            return casts;
        }
    }
}
