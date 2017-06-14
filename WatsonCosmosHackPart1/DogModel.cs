using System;
using Newtonsoft.Json;

namespace WatsonCosmosHackPart1
{
    public class DogModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("furColor")]
        public string FurColor { get; set; }
    }
}