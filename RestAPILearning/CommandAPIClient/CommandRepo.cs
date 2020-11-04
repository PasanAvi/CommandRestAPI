using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CommandAPIClient
{
    class CommandRepo
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public Owner Owner { get; set; }
    }
    
    public class Owner
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("node_id")]
        public string NodeId { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }
}
