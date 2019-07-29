using Application.Features.Card;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace UnitTestApplication
{
    [TestClass]
    public class UnitTestFeatures
    {
        CancellationTokenSource source;
        CancellationToken token;
        IMediator mediator;

        [TestInitialize]
        public void Init()
        {
            source = new CancellationTokenSource();
            token = source.Token;            
        }

        [TestMethod]
        public async Task TestGetCards()
        {
            var getCardsQuery = new GetCardsQuery();
        }
    }
}
