using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpChain.Domain;

namespace SharpChain.Tests
{
	[TestClass]
	public class TransactionTests
	{
		[TestMethod]
		public void CanAddTransaction()
		{
			var controller = new ChainAuthority();

			var index = controller.CreateTransaction("ABC", "DEF", 1500432);

			var id = Guid.NewGuid();
			controller.Mine(id.ToString());

			var transaction = controller.GetFullChain()[index].Transactions
				.FirstOrDefault(d => d.Sender == "ABC" && d.Recipient == "DEF" && d.Amount == 1500432);

			Assert.IsNotNull(transaction);
		}
	}
}