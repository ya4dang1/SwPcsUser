using Core.Libraries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Modules.Card
{
    public class GetCardInformationQuery: IRequest<GetCardInformationQueryResponse>
    {
    }

    public class GetCardInformationQueryResponse: BaseResponse
    {
    }

    public class GetCardInformationQueryHandler : IRequestHandler<GetCardInformationQuery, GetCardInformationQueryResponse>
    {
        public Task<GetCardInformationQueryResponse> Handle(GetCardInformationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
