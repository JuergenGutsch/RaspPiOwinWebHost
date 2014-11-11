using System;
using Microsoft.Owin.Hosting;
using Mono.Options;

namespace OwinSelfHost
{
	class MainClass
	{
		public static void Main(params string[] args)
		{
			var port = 3000;
			var p = new OptionSet()
				.Add("p|port=", v => port = int.Parse(v))
				.Parse(args);

			var uri = String.Format("http://localhost:{0}/", port);

            using (WebApp.Start<Startup3>(uri))
			{
				Console.WriteLine("Started");
				Console.WriteLine("Listening on Port {0}", port);
				Console.ReadKey();
				Console.WriteLine("Stopping");
			}
		}
	}
}
