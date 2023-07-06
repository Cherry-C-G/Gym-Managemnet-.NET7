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

namespace GymMasterPro.Pages.Memberships
{
    public class DeleteModel : PageModel
    {
        private readonly IMembershipService _membershipService;

        public DeleteModel(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [BindProperty]
      public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var membership = await _membershipService.GetById( id);

            if (membership == null)
            {
                return NotFound();
            }
            else 
            {
                Membership = membership;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var membership = await _membershipService.DeleteAsync(id);

            await _membershipService.DeleteAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
