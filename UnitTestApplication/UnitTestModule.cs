using Application.Modules.Test;
using Application.Modules.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OmniPay;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestModule
    {
        CancellationTokenSource source;
        CancellationToken token;
        WSCrystalPaymentsSvcSoapClient ws;

        [TestInitialize]
        public void Init()
        {
            source = new CancellationTokenSource();
            token = source.Token;
            ws = new WSCrystalPaymentsSvcSoapClient(WSCrystalPaymentsSvcSoapClient.EndpointConfiguration.WSCrystalPaymentsSvcSoap12);
        }

        [TestMethod]
        public async Task TestTestConnectionCommandAsync()
        {
            var handler = new TestConnectionCommandHandler(ws);
            var result = await handler.Handle(new TestConnectionCommand { EchoTest = "BaBaBlackSheep" }, token);
            StringAssert.Equals("BaBaBlackSheep", result.Result);
        }

        [TestMethod]
        public async Task TestCheckUserExistsCommandAsync()
        {
            var handler = new CheckUserExistsCommandHandler(ws);
            var result = await handler.Handle(
                    new CheckUserExistsCommand
                    {
                         UserId = "sheep"
                    },
                    token
                );
           
            Assert.IsFalse(result.Success);
            Assert.IsFalse(result.IsError);

        }

        [TestMethod]
        public async Task TestRegisterNewUserCommandAsync()
        {
            var handler = new RegisterNewUserCommandHandler(ws);
            var result = await handler.Handle(
                new RegisterNewUserCommand
                {

                }, token
            );

            Assert.IsTrue(result.Success);

        }
    }
}
