using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SharpChain.Domain
{
	public class Blockchain: IEnumerable<Block>
	{
		private readonly List<Block> chain;
		private readonly List<Transaction> transactions;

		// The amount of zeros that must be at the end of the hash for it to be valid.
		// Used to verify Proof of Work.
		private const int Difficulty = 4;

		public Block LastBlock
		{
			get { return chain.Last(); }
		}

		public int Length
		{
			get { return chain.Count; }
		}

		public Block this[int index]
		{
			get
			{
				if (index >= 0 && index < chain.Count)
				{
					return chain[index];
				}

				if (index == -1)
				{
					return LastBlock;
				}

				return null;
			}
		}

		public Blockchain()
		{
			chain = new List<Block>();
			transactions = new List<Transaction>();

			CreateNewBlock(100);
		}

		public void CreateNewBlock(long proof)
		{
			var block = new Block()
			{
				Index = chain.Count,
				Proof = proof,
				PreviousHash = chain.Count == 0 ? "1" : HashBlock(chain.Last()),
				Transactions = new List<Transaction>(transactions),
				TimeStamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
			};

			transactions.Clear();

			chain.Add(block);
		}

		public int CreateNewTransaction(string sender, string recipient, decimal amount)
		{
			var transaction = new Transaction()
			{
				Amount = amount,
				Recipient = recipient,
				Sender = sender
			};

			transactions.Add(transaction);

			// Return the index for the next block to be mined, thats where the transaction will be added to
			return LastBlock.Index + 1;
		}

		public static string HashBlock(Block block)
		{
			var json = JsonConvert.SerializeObject(block, Formatting.None);

			using (var sha = SHA256.Create())
			{
				var enc = Encoding.UTF8;
				var result = sha.ComputeHash(enc.GetBytes(json));

				return ConvertHashToString(result);
			}
		}

		public long ProofOfWork(long lastProof)
		{
			var proof = 0;

			while (!ValidProof(lastProof, proof))
			{
				proof++;
			}

			return proof;
		}

		public bool ValidProof(long lastProof, long proof)
		{
			using (var sha = SHA256.Create())
			{
				var enc = Encoding.UTF8;
				var result = sha.ComputeHash(enc.GetBytes($"{lastProof}{proof}"));

				var hash = ConvertHashToString(result);
				return hash.EndsWith(new string('0', Difficulty));
			}
		}

		private static string ConvertHashToString(byte[] hash)
		{
			var sb = new StringBuilder();

			foreach (var t in hash)
			{
				sb.Append(t.ToString("x2"));
			}

			return sb.ToString();
		}

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}

		public IEnumerator<Block> GetEnumerator()
		{
			return chain.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

}
