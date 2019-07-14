using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Card
{
    public class ViewCardBalanceQuery: IRequest<ViewCardBalanceQueryResponse>
    {
    }

    public class ViewCardBalanceQueryResponse: BaseResponse
    {
    }

    public class ViewCardBalanceQueryHandler : IRequestHandler<ViewCardBalanceQuery, ViewCardBalanceQueryResponse>
    {
        public Task<ViewCardBalanceQueryResponse> Handle(ViewCardBalanceQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

}
