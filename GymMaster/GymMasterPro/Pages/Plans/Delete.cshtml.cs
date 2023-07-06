﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using Data;
using Services.Interfaces;

namespace GymMasterPro.Pages.Plans
{
    public class DeleteModel : PageModel
    {
        private readonly IPlanService _planService;

        public DeleteModel(IPlanService planService)
        {
            _planService = planService;
        }

        [BindProperty]
      public Plan Plan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var plan = await _planService.GetById(id);

            if (plan == null)
            {
                return NotFound();
            }
            else 
            {
                Plan = plan;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var plan = await _planService.GetById(id);

            if (plan != null)
            {
                Plan = plan;
               
                await _planService.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
