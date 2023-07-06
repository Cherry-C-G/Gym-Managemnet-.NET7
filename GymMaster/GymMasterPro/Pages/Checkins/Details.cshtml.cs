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

namespace GymMasterPro.Pages.Checkins
{
    public class DetailsModel : PageModel
    {
        private readonly ICheckinService _checkinService;

        public DetailsModel(ICheckinService checkinService)
        {
            _checkinService = checkinService;
        }

      public Checkin Checkin { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var checkin = await _checkinService.GetById( id);
            if (checkin == null)
            {
                return NotFound();
            }
            else 
            {
                Checkin = checkin;
            }
            return Page();
        }
    }
}
