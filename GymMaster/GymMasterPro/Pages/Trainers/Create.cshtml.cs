﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities;
using Data;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace GymMasterPro.Pages.Trainers
{
    public class CreateModel : PageModel
    {
        private readonly ITrainerService _trainerService;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(ITrainerService trainerService, UserManager<IdentityUser> userManager)
        {
            _trainerService = trainerService;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Trainer Trainer { get; set; } = default!;
        public UserManager<IdentityBuilder> UserManager { get; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid  || Trainer == null)
            {
                return Page();
            }
          var loggedInUser = await _userManager.GetUserAsync(User);
            if(loggedInUser ==null)
            {
                return Page();
            }
            Trainer.UpdateAt = DateTime.Now;
            Trainer.CreatedAt = DateTime.Now;
            Trainer.CreatedBy = loggedInUser?.UserName;

          
            await _trainerService.SaveAsync(Trainer);

            return RedirectToPage("./Index");
        }
    }
}
