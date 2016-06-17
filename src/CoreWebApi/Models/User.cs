using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#pragma warning disable 1591

namespace CoreWebAPI.Models
{
    /// <summary>
    /// Represents a user 
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        [JsonProperty("ip_address")]
        public string IPAddress { get; set; }
    }
}

#pragma warning restore 1591