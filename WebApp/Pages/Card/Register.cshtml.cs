﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Card;
using Application.Features.User;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApp.Pages.Card
{   
    public class RegisterModel : PageModelBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public RegisterModel(IMediator mediator)
        {
            this.mediator = mediator;

            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<InputModel, RegisterCardCommand>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            public string CardNumber { get; set; }

            [Required]
            public int CVV { get; set; }

            [Required]
            public int PIN { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM'/'yyyy}")]
            [JsonConverter(typeof(IsoDateTimeConverter), "MM'/'yyyy")]
            public DateTime ExpiredDate { get; set; }
        }

        public void OnGet()
        {
            Input = new InputModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var registerCardCommand = mapper.Map<RegisterCardCommand>(Input);
                var result = await mediator.Send(registerCardCommand);
                if (!result.IsError)
                {
                    return Redirect("/Card/?toast=success");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }                
            }

            return Page();
        }
    }
}