using Newtonsoft.Json;

namespace SharpChain.Domain
{
	public class Transaction
	{
		[JsonProperty(Order = 1)]
		public string Sender { get; set; }

		[JsonProperty(Order = 2)]
		public string Recipient { get; set; }

		[JsonProperty(Order = 3)]
		public decimal Amount { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
