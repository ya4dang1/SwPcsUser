﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Pages;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageModelBase
    {
        public void OnGet()
        {

        }
    }
}
