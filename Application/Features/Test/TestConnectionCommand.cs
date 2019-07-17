using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Test
{
    public class TestConnectionCommand : IRequest<TestConnectionCommandResponse>
    {
        public string EchoTest { get; set; }
    }

    public class TestConnectionCommandResponse : BaseResponse
    {
        public string Result { get; set; }
    }

    public class TestConnectionCommandHandler : IRequestHandler<TestConnectionCommand, TestConnectionCommandResponse>
    {
        

        public TestConnectionCommandHandler()
        {         
        }

        public Task<TestConnectionCommandResponse> Handle(TestConnectionCommand request, CancellationToken cancellationToken)
        {
            var response = new TestConnectionCommandResponse();          

            return Task.FromResult(response);
        }
    }
}
