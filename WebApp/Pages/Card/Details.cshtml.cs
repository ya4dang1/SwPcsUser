using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Card;
using Application.Models;
using EmyralSystems.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Card
{
    public class DetailsModel : PageModel
    {
        private readonly IMediator mediator;

        public DetailsModel(IMediator mediator)
        {
            this.mediator = mediator;           
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public UserCard Card { get; set; }
            public Dictionary<string, double> Balance { get; set; }
            public List<CardTransaction> Transaction { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Input = new InputModel();

            var getCardDetailsQuery = new GetCardDetailsQuery { Id = id };
            var result = await mediator.Send(getCardDetailsQuery);

            if (!result.IsError)
            {
                Input.Card = result.Card;
                Input.Balance = result.Balance;
                Input.Transaction = result.Transaction;
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return Page();
        }
    }
}