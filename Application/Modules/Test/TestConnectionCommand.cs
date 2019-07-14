using OmniPay;
using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Test
{
    public class TestConnectionCommand: IRequest<TestConnectionCommandResponse>
    {
        public string EchoTest { get; set; }
    }

    public class TestConnectionCommandResponse: BaseResponse
    {
        public string Result { get; set; }
    }

    public class TestConnectionCommandHandler : IRequestHandler<TestConnectionCommand, TestConnectionCommandResponse>
    {
        private readonly WSCrystalPaymentsSvcSoapClient ws;

        public TestConnectionCommandHandler(WSCrystalPaymentsSvcSoapClient ws)
        {
            this.ws = ws;
        }

        public Task<TestConnectionCommandResponse> Handle(TestConnectionCommand request, CancellationToken cancellationToken)
        {
            var response = new TestConnectionCommandResponse();
            try
            {
                var result = ws.TestConnectionAsync(request.EchoTest).Result;
                response.Result = result.Body.TestConnectionResult;
            }
            catch (Exception ex)
            {
                response.AddError(ex);
            }            

            return Task.FromResult(response);
        }
    }
}
