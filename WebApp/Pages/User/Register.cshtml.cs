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
            public string LastName { get; set; }
                        
            public string FirstName { get; set; }
                        
            public string MiddleName { get; set; }
                        
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd'/'MM'/'yy}")]
            [JsonConverter(typeof(DateTime), "dd'/'MM'/'yy")]
            public DateTime Birthday { get; set; }
                        
            public string AddressType { get; set; }
                        
            public string Address { get; set; }
                        
            public string City { get; set; }

            public string Region { get; set; }
                        
            public string Zip { get; set; }
                        
            public string Mobile { get; set; }

            public string IDValue { get; set; }

            [Required]
            [Display(Name = "Email")]
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