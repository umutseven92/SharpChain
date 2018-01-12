using System;
using System.Diagnostics;
using System.Reflection;
using log4net;

namespace SharpChain.Domain
{
	public class Controller
	{
		private readonly Blockchain _chain;
		private readonly Guid _nodeId;


		public Controller()
		{
			_chain = new Blockchain();
			_nodeId = Guid.NewGuid();
		}

		public void Mine()
		{
			var lastBlock = _chain.LastBlock;
			var lastProof = lastBlock.Proof;

			var proof = _chain.ProofOfWork(lastProof);

			CreateTransaction("0", _nodeId.ToString(), 1);

			_chain.CreateNewBlock(proof);
		}

		public int CreateTransaction(string sender, string recipient, decimal amount)
		{
			var index = _chain.CreateNewTransaction(sender, recipient, amount);

			return index;
		}

		public string GetFullChainJson()
		{
			return _chain.ToString();
		}

		public Blockchain GetFullChain()
		{
			return _chain;
		}

		public Block GetLastBlock()
		{
			return _chain.LastBlock;
		}
		
	}
}
