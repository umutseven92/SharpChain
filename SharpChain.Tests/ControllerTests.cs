using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpChain.Domain;

namespace SharpChain.Tests
{
	[TestClass]
	public class Tests
	{
		[TestMethod]
		public void CanMine()
		{
			var controller = new Controller();

			controller.Mine();
			controller.Mine();

			var chain = controller.GetFullChain();

			Assert.AreEqual(chain.Length, 3);
		}

		[TestMethod]
		public void CanAddTransaction()
		{
			var controller = new Controller();

			var index = controller.CreateTransaction("ABC", "DEF", 1500432);
			controller.Mine();

			var transaction = controller.GetFullChain()[index].Transactions
				.FirstOrDefault(d => d.Sender == "ABC" && d.Recipient == "DEF" && d.Amount == 1500432);

			Assert.IsNotNull(transaction);
		}
	}
}
