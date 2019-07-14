using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Modules.Test;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Test
{
    public class TestConnectionModel : PageModel
    {
        private readonly IMediator mediator;

        public TestConnectionModel(IMediator mediator)
        {
            this.mediator = mediator;           
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string TestEcho { get; set; }
            public string Result { get; set; }
        }

        public void OnGet()
        {
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var testConnectionCommand = new TestConnectionCommand { EchoTest = Input.TestEcho };
            var result = await mediator.Send(testConnectionCommand);

            if (result.IsError)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                Input.Result = result.Result;
            }

            return Page();
        }
    }
}