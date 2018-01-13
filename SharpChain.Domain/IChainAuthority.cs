namespace SharpChain.Domain
{
	public interface IChainAuthority
	{
		decimal Mine(string id);

		int CreateTransaction(string sender, string recipient, decimal amount);

		string GetFullChainJson();

		Blockchain GetFullChain();

		Block GetLastBlock();

		decimal GetBalance(string id);

		bool CanSend(string id, decimal amount);
	}
}
