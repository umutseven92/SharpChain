using System.Linq;

namespace SharpChain.Domain
{
	public class ChainAuthority : IChainAuthority
	{
		private readonly Blockchain _chain;
		private const decimal Reward = 1;

		public ChainAuthority()
		{
			_chain = new Blockchain();
		}

		public decimal Mine(string id)
		{
			var lastBlock = _chain.LastBlock;
			var lastProof = lastBlock.Proof;

			var proof = _chain.ProofOfWork(lastProof);

			// Set the sender to M to signify it was mined.
			CreateTransaction("M", id, Reward);

			_chain.CreateNewBlock(proof);

			return Reward;
		}

		public decimal GetBalance(string id)
		{
			var balance = 0m;

			foreach (var block in _chain)
			{
				var recipientTransactions = block.Transactions.Where(t => t.Recipient.Equals(id));

				// Add the amount from each transaction where id is the recipient
				balance += recipientTransactions.Sum(transaction => transaction.Amount);

				var senderTransactions = block.Transactions.Where(t => t.Sender.Equals(id));
				
				// Subtract the amount from each transaction where id is the sender 
				balance = senderTransactions.Aggregate(balance, (current, transaction) => current - transaction.Amount);
			}

			return balance;
		}

		/// <summary>
		/// Check if id can send amount.
		/// </summary>
		public bool CanSend(string id, decimal amount)
		{
			var balance = GetBalance(id);
			return balance >= amount;
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
