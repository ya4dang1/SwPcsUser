using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Card;
using Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Card
{
    public class UnregisterModel : PageModelBase
    {
        private readonly IMediator mediator;

        public UnregisterModel(IMediator mediator)
        {
            this.mediator = mediator;            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public Guid Id { get; set; } //Implement as hidden field

            [Display(Name = "CardNumber")]
            public string CardNumber { get; set; }

            [Display(Name = "ExpiredDate")]
            public DateTime ExpiredDate { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Input = new InputModel { Id = id };

            var getCardDetailsQuery = new GetCardDetailsQuery { Id = id };
            var result = await mediator.Send(getCardDetailsQuery);

            if(!result.IsError)
            {
                Input.CardNumber = result.Card.CardNumber;
                Input.ExpiredDate = result.Card.ExpiredDate;
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

        public async Task<IActionResult> OnPostAsync(Guid id)
        {
            if (Input.Id != id)
                return NotFound();

            var unregisterCardCommand = new UnregisterCardCommand { Id = id };
            var result = await mediator.Send(unregisterCardCommand);

            if (!result.IsError)
            {
                return RedirectToPage("/Card/Index", new { toast = "success" });
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