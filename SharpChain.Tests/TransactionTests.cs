using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			var controller = new Controller();

			var index = controller.CreateTransaction("ABC", "DEF", 1500432);
			controller.Mine();

			var transaction = controller.GetFullChain()[index].Transactions
				.FirstOrDefault(d => d.Sender == "ABC" && d.Recipient == "DEF" && d.Amount == 1500432);

			Assert.IsNotNull(transaction);
		}
	}
}