using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpChain.Domain;

namespace SharpChain.Console.Controllers
{
	[Route("SharpChain")]
	public class ChainController : Controller
	{
		private readonly ILogger _logger;
		private readonly IChainAuthority _chainAuthority;

		public ChainController(ILogger<ChainController> logger, IChainAuthority chainAuthority)
		{
			_logger = logger;
			_chainAuthority = chainAuthority;
		}

		[HttpGet("Register")]
		public string Register()
		{
			var id = Guid.NewGuid();

			return $"Your ID is {id.ToString()}\nPlease keep this hidden!";
		}

		[HttpPost("Mine/{id}")]
		public string Mine([FromRoute]string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return "Please provide your ID with your request.";
			}

			var reward = _chainAuthority.Mine(id);

			return $"A Block was mined. {reward} SharpCoin has been awarded to {id}.";
		}

		[HttpGet("GetBalance/{id}")]
		public decimal GetBalance([FromRoute] string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return 0;
			}

			var balance = _chainAuthority.GetBalance(id);

			return balance;
		}

		[HttpPost("SendCoin/{senderId}/{recipientId}/{amount}")]
		public string SendCoin([FromRoute]string senderId, [FromRoute]string recipientId, [FromRoute]decimal amount)
		{
			if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(recipientId) || amount <= 0)
			{
				return "Please provide a valid ID and amount with your request.";
			}

			var canSend = _chainAuthority.CanSend(senderId, amount);

			if (!canSend)
			{
				return $"Your balance is not sufficient to send {amount}.";
			}

			_chainAuthority.CreateTransaction(senderId, recipientId, amount);

			return $"{amount} SharpCoin has been sent to {recipientId}.";
		}

		[HttpGet("GetChain")]
		public string GetFullChain()
		{
			return _chainAuthority.GetFullChainJson();
		}
	}
}
