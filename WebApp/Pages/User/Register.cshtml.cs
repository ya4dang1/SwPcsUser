using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.User;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApp.Pages.User
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
                config.CreateMap<InputModel, UpdateUserProfileCommand>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Prompt = "FirstName")]
            public string FirstName { get; set; }

            [Display(Prompt = "MiddleName")]
            public string MiddleName { get; set; }

            [Display(Prompt = "LastName")]
            public string LastName { get; set; }

            [Display(Prompt = "Birthday")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yy}")]
            [JsonConverter(typeof(DateTime), "dd'/'MM'/'yy")]
            public DateTime Birthday { get; set; }

            [Display(Prompt = "AddressType")]
            public string AddressType { get; set; }

            [Display(Prompt = "Address")]
            public string Address { get; set; }

            [Display(Prompt = "City")]
            public string City { get; set; }

            [Display(Prompt = "Region")]
            public string Region { get; set; }

            [Display(Prompt = "Zip")]
            public string Zip { get; set; }

            public string Mobile { get; set; }

            [Display(Prompt = "IDValue")]
            public string IDValue { get; set; }

            [Required]
            [Display(Name = "Email", Prompt = "Email")]
            public string Email { get; set; }
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
            var updateUserProfile = mapper.Map<UpdateUserProfileCommand>(Input);
            var result = await mediator.Send(updateUserProfile);

            if (!result.IsError)
            {
                return Redirect("/");               
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return Page();
        }
    }
}