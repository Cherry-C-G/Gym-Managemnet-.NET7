using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Entities;
using Data;
using Services.Interfaces;

namespace GymMasterPro.Pages.Trainers
{
    public class DeleteModel : PageModel
    {
        private readonly ITrainerService _trainerService;

        public DeleteModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [BindProperty]
      public Trainer Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var trainer = await _trainerService.GetById(id);

            if (trainer == null)
            {
                return NotFound();
            }
            else 
            {
                Trainer = trainer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id ==0)
            {
                return NotFound();
            }
            var trainer = await _trainerService.GetById(id);

            if (trainer != null)
            {
                Trainer = trainer;
                await _trainerService.DeleteAsync(id);
            }

            return RedirectToPage("./Index");
        }
    }
}
