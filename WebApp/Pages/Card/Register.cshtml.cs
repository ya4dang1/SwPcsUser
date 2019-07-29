using System;
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
            [Required(ErrorMessage = "The {0} field is required.")]
            [Display(Name = "CardNumber", Prompt = "")]
            public string CardNumber { get; set; }

            [Required(ErrorMessage = "The {0} field is required.")]
            [Display(Name = "CVV", Prompt = "")]
            public int CVV { get; set; }

            [Required(ErrorMessage = "The {0} field is required.")]
            [Display(Name = "PIN", Prompt = "")]
            public int PIN { get; set; }

            [Required(ErrorMessage = "The {0} field is required.")]
            [Display(Name = "ExpiredDate", Prompt = "")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM'/'yyyy}")]
            [JsonConverter(typeof(IsoDateTimeConverter), "MM'/'yyyy")]
            public DateTime ExpiredDate { get; set; }
        }

        public void OnGet()
        {
            
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