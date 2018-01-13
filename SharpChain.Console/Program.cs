using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SharpChain.Console
{
	internal class Program
	{
		private static void Main()
		{
			BuildWebHost().Run();
		}

		public static IWebHost BuildWebHost(string[] args = null) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.UseUrls("http://localhost:9532")
				.Build();

	}
}
