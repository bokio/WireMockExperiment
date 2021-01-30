using System;
using System.Net.Http;
using System.Threading.Tasks;
using WireMock.Admin.Requests;
using WireMock.Logging;
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
                Logger = new DebuggableLogger(),
                
            });
            server.Given(
                    WireMock.RequestBuilders.Request.Create().WithPath("/test"))
                .RespondWith(WireMock.ResponseBuilders.Response.Create().WithBody("Test"));

            var client = new HttpClient();
            var res = await client.GetStringAsync("http://localhost:50100/test");
            Assert.Equal("Test", res);
        }
    }

    public class DebuggableLogger : WireMock.Logging.IWireMockLogger
    {
        private WireMockConsoleLogger inner;

        public DebuggableLogger()
        {
            this.inner = new WireMock.Logging.WireMockConsoleLogger();
        }
        public void Debug(string formatString, params object[] args)
        {
            inner.Debug(formatString, args);
        }

        public void DebugRequestResponse(LogEntryModel logEntryModel, bool isAdminRequest)
        {
            inner.DebugRequestResponse(logEntryModel, isAdminRequest);
        }

        public void Error(string formatString, params object[] args)
        {
            inner.Error(formatString, args);
        }

        public void Error(string formatString, Exception exception)
        {
            inner.Error(formatString, exception);
        }

        public void Info(string formatString, params object[] args)
        {
            inner.Info(formatString, args);
        }

        public void Warn(string formatString, params object[] args)
        {
            inner.Warn(formatString, args);
        }
    }
}
