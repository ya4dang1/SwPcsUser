using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Modules.User;
using AutoMapper;
using MediatR;
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

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<InputModel, RegisterNewUserCommand>();
            });
            mapper = mapperConfig.CreateMapper();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            public string Result { get; set; }

            [Required]
            public string UserId { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string MiddleName { get; set; }

            [Required]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yy}")]
            [JsonConverter(typeof(DateTime), "dd'/'MM'/'yy")]
            public DateTime Birthday { get; set; }

            [Required]
            public string AddressType { get; set; }

            [Required]
            public string Address { get; set; }

            [Required]
            public string City { get; set; }

            public string Region { get; set; }

            [Required]
            public string Zip { get; set; }

            [Required]
            public bool IsDeliveryYN { get; set; }

            [Required]
            public string Mobile { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public bool CardRequestYN { get; set; }

            public string CardPersoName { get; set; }

            [Required]
            public string FundSource { get; set; }

            [Required]
            public string Industry { get; set; }

            [Required]
            public string Subindustry { get; set; }

            public string Comments { get; set; }

            [Required]
            public string IDValue { get; set; }

            [Required]
            public string IDType { get; set; }

            [Required]
            public DateTime IDIssuanceDate { get; set; }

            [Required]
            public DateTime IDExpiryDate { get; set; }

            [Required]
            public string DeliveryCountry { get; set; }
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
            var registerNewUserCommand = mapper.Map<RegisterNewUserCommand>(Input);
            var result = await mediator.Send(registerNewUserCommand);

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