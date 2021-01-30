using System.Net.Http;
using System.Threading.Tasks;
using WireMock.Server;
using Xunit;

namespace WireMockExperiment
{
    public class Class1
    {
        [Fact]
        public async Task DummyTest()
        {
            var server = WireMockServer.Start(new WireMock.Settings.WireMockServerSettings
            {
                Port = 50100,
                Logger = new WireMock.Logging.WireMockConsoleLogger(),
                
            });
            server.Given(
                    WireMock.RequestBuilders.Request.Create().WithPath("/test"))
                .RespondWith(WireMock.ResponseBuilders.Response.Create().WithBody("Test"));

            var client = new HttpClient();
            var res = await client.GetStringAsync("http://localhost:50100/test");
            Assert.Equal("Test", res);
        }
    }
}
