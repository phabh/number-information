using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NumberInformation.Models
{
    public class NIResponse
    {

        [JsonPropertyName("input")]
        public int Input { get; set; }

        [JsonPropertyName("dividing_numbers")]
        public IEnumerable<int> DividingNumbers { get; set; }

        [JsonPropertyName("dividing_prime")]
        public IEnumerable<int> DividingPrime { get; set; }
    }
}
