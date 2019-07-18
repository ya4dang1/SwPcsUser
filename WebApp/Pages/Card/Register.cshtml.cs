﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Modules.Card;
using Application.Modules.User;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Card
{
    public class RegisterModel : PageModelBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RegisterModel(IMediator mediator)
        {
            this.mediator = mediator;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InputModel, GetCardInformationQuery>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel : GetCardInformationQuery
        {
            public string Result { get; set; }
        }

        public void OnGet()
        {
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            ///NOTE:For massive fields, use AutoMapper to map the variables
            var getCardInformationQuery = mapper.Map<GetCardInformationQuery>(Input);
            var result = await mediator.Send(getCardInformationQuery);

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