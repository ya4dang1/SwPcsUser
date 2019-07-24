using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Features.User;
using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Resources;

namespace WebApp.Pages.User
{     
    public class UpdateProfileModel : PageModelBase
    {
        private readonly IMediator mediator;
        private readonly LoginUser loginUser;
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public UpdateProfileModel(IMediator mediator, LoginUser loginUser, ApplicationDbContext dbContext)
        {
            this.mediator = mediator;
            this.loginUser = loginUser;
            this.dbContext = dbContext;
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<InputModel, UpdateUserProfileCommand>();
                config.CreateMap<UserProfile, InputModel>();
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
            
            [Display(Name= "Birthday", Prompt = "Birthday")]
            public DateTime? Birthday { get; set; }

            [Display(Name="", Prompt = "AddressType")]
            public string AddressType { get; set; }

            [Display(Name = "Address", Prompt = "Address")]
            public string Address { get; set; }

            [Display(Prompt = "City")]
            public string City { get; set; }

            [Display(Prompt = "Region")]
            public string Region { get; set; }

            [Display(Prompt = "Zip")]
            public string Zip { get; set; }

            [Display(Name ="Mobile", Prompt ="Mobile")]
            public string Mobile { get; set; }

            [Display(Name="ID", Prompt = "ID")]
            public string IDValue { get; set; }

            [Required]
            [Display(Name = "Email", Prompt = "Email")]
            public string Email { get; set; }

            [Display(Name = "FullName")]
            public string FullName
            {
                get { return $"{LastName} {MiddleName} {FirstName}";  }
            }
        }

        public async Task OnGetAsync()
        {
            var user = await loginUser.GetAsync();
            var userProfile = await dbContext.UserProfiles.FirstOrDefaultAsync(fd => fd.User == user);

            Input = new InputModel();
            Input.Email = user.Email;
                       
            if(userProfile != null)
            {
                Input = mapper.Map<UserProfile, InputModel>(userProfile, Input);
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            ///NOTE:For massive fields, use AutoMapper to map the variables
            var updateUserProfileCommand = mapper.Map<UpdateUserProfileCommand>(Input);
            var result = await mediator.Send(updateUserProfileCommand);

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