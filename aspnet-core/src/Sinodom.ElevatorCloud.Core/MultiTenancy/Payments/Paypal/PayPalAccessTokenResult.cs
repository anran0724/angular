using Newtonsoft.Json;

namespace Sinodom.ElevatorCloud.MultiTenancy.Payments.Paypal
{
    public class PayPalAccessTokenResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
