using System.Collections.Generic;
using Newtonsoft.Json;

namespace SharpChain.Domain
{
	public class Block
	{
		[JsonProperty(Order = 1)]
		public int Index { get; set; }

		[JsonProperty(Order = 2)]
		public long TimeStamp { get; set; }

		[JsonProperty(Order = 3)]
		public List<Transaction> Transactions { get; set; }

		[JsonProperty(Order = 4)]
		public long Proof { get; set; }

		[JsonProperty(Order = 5)]
		public string PreviousHash { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
