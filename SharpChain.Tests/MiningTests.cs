using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpChain.Domain;

namespace SharpChain.Tests
{
	[TestClass]
	public class MiningTests
	{
		[TestMethod]
		public void CanMine()
		{
			var controller = new ChainAuthority();

			var id = Guid.NewGuid();
			controller.Mine(id.ToString());

			var chain = controller.GetFullChain();

			Assert.AreEqual(chain.Length, 2);
		}

		[TestMethod]
		[Ignore]
		public void MiningBenchmark()
		{
			var controller = new ChainAuthority();
			var id = Guid.NewGuid();
			const int mineAmount = 10;

			TimeSpan span;
			var watch = new Stopwatch();

			for (var i = 0; i < mineAmount; i++)
			{
				watch.Start();
				controller.Mine(id.ToString());
				watch.Stop();
				span += watch.Elapsed;

				watch.Reset();
			}

			span /= mineAmount;

			var average = span.TotalSeconds;

			Console.WriteLine($"Average mining time is {average} seconds for {mineAmount} mined blocks.");
		}
	}
}
