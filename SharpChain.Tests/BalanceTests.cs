using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpChain.Domain;

namespace SharpChain.Tests
{
	[TestClass]
	public class BalanceTests
	{
		[TestMethod]
		public void CanGetBalance()
		{
			var id = Guid.NewGuid();
			const int amount = 3;

			var controller = new ChainAuthority();

			for (var i = 0; i < amount; i++)
			{
				controller.Mine(id.ToString());
			}

			var balance = controller.GetBalance(id.ToString());

			Assert.AreEqual(Convert.ToDecimal(amount), balance);
		}

		[TestMethod]
		public void CantSend()
		{
			var id = Guid.NewGuid();
			const int amount = 3;

			var controller = new ChainAuthority();

			for (var i = 0; i < amount; i++)
			{
				controller.Mine(id.ToString());
			}

			var canSend = controller.CanSend(id.ToString(), 5);

			Assert.IsFalse(canSend);
		}

		[TestMethod]
		public void CanSend()
		{
			var id = Guid.NewGuid();
			const int amount = 3;

			var controller = new ChainAuthority();

			for (var i = 0; i < amount; i++)
			{
				controller.Mine(id.ToString());
			}

			var canSend = controller.CanSend(id.ToString(), 2);

			Assert.IsTrue(canSend);

		}
	}
}
