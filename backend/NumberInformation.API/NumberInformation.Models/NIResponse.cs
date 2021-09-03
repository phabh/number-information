using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace NumberInformation.Models
{
    public class NIResponse
    {

        [JsonPropertyName("input")]
        public long Input { get; set; }

        [JsonPropertyName("dividing_numbers")]
        public IEnumerable<long> DividingNumbers { get; set; }

        [JsonPropertyName("dividing_prime")]
        public IEnumerable<long> DividingPrime { get; set; }
    }
}
