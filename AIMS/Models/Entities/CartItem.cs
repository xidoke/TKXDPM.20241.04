using Newtonsoft.Json;

namespace AIMS.Models.Entities
{
    public class CartItem
    {
        [JsonProperty("isSelected")]
        public bool isSelected { get; set; }
        [JsonProperty("media_id")]
        public int media_id { get; set; }

        [JsonProperty("media_title")]
        public string media_title { get; set; }
        [JsonProperty("total_money")]
        public int total_money { get; set; }

        [JsonProperty("quantity")]
        public int quantity { get; set; }

        [JsonProperty("isPossibleToPlaceOrder")]
        public string isPossibleToPlaceOrder { get; set; }
    }
}