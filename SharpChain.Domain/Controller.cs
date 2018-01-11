using System;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace SharpChain.Domain
{
	public class Controller
	{
		private readonly Blockchain chain;
		private readonly Guid NodeID;

		private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


		public Controller()
		{
			chain = new Blockchain();
			NodeID = Guid.NewGuid();
		}

		public void Mine()
		{
			var lastBlock = chain.LastBlock;
			var lastProof = lastBlock.Proof;

			Log.Info("Mining new coin..");

			var watch = new Stopwatch();

			watch.Start();
			var proof = chain.ProofOfWork(lastProof);
			watch.Stop();

			Log.Info($"New block mined. It took {watch.Elapsed.Seconds}.{watch.ElapsedMilliseconds} seconds.");

			CreateTransaction("0", NodeID.ToString(), 1);

			chain.CreateNewBlock(proof);
		}

		public int CreateTransaction(string sender, string recipient, decimal amount)
		{
			var index = chain.CreateNewTransaction(sender, recipient, amount);

			return index;
		}

		public string GetFullChainJson()
		{
			return chain.ToString();
		}

		public Blockchain GetFullChain()
		{
			return chain;
		}

		public Block GetLastBlock()
		{
			return chain.LastBlock;
		}
		
	}
}
