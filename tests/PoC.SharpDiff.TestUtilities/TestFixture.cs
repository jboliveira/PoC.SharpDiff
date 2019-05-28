using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace PoC.SharpDiff.TestUtilities
{
	public class TestFixture<TStartup> : IDisposable
    {
        private readonly TestServer Server;
        public HttpClient Client { get; }

		public TestFixture()
            : this("Testing")
        { }

        protected TestFixture(string environment)
        {
            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseEnvironment(environment)
                .UseStartup(typeof(TStartup));

            // Create instance of test server
            Server = new TestServer(webHostBuilder);

            // Add configuration for client
            Client = Server.CreateClient();
            Client.BaseAddress = new Uri("https://localhost:5001");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(TStartup).GetTypeInfo().Assembly;

            var manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                },
                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider()
                }
            };

            services.AddSingleton(manager);
        }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			Client.Dispose();
			Server.Dispose();
		}
	}
}