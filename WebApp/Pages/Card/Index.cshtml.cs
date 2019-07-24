﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Card;
using Application.Features.User;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Card
{
    [Authorize(Policy = "IsApproved")]
    public class IndexModel : PageModelBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;

            var mapperConfig = new MapperConfiguration(cfg =>
            {               
            });
            mapper = mapperConfig.CreateMapper();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "CardNumber")]
            public string CardNumber { get; set; }

            [Display(Name = "CardNumber")]
            public string UserId { get; set; }

            [Display(Name = "CVV")]
            public int CVV { get; set; }

            [Display(Name = "PIN")]
            public int PIN { get; set; }

            [Display(Name = "ExpiredDate")]
            public DateTime ExpiredDate { get; set; }

            public List<UserCard> Cards { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Input = new InputModel();
            var request = new GetCardsQuery();
            var result = await mediator.Send(request);

            if (!result.IsError)
            {
                Input.Cards = result.Cards;
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