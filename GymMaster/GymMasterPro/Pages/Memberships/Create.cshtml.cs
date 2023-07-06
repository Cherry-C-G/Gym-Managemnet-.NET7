using System;
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

namespace GymMasterPro.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly IMembershipService _membershipService;
        private readonly IMemberService _memberService;
        private readonly IPlanService _planService;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(IMemberService memberService,IPlanService planService,IMembershipService membershipService, UserManager<IdentityUser> userManager)
        {
            _memberService = memberService;
            _planService = planService;
            _membershipService = membershipService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var members = await _memberService.GetMembers();
            var plans = await _planService.GetPlans();
        ViewData["MemberId"] = new SelectList(members, "Id", "FirstName");
        ViewData["PlanId"] = new SelectList(plans, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Membership == null)
            {
                return Page();
            }
            var loggedInUser = await _userManager.GetUserAsync(User);
            if (loggedInUser == null)
            {
                return Page();
            }
            Membership.UpdateAt = DateTime.Now;
            Membership.CreatedAt = DateTime.Now;
            Membership.CreatedBy = loggedInUser?.UserName;

            await _membershipService.SaveAsync(Membership);

            return RedirectToPage("./Index");
        }
    }
}
