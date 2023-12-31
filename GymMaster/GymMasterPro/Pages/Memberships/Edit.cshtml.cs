﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Entities;
using Data;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace GymMasterPro.Pages.Memberships
{
    public class EditModel : PageModel
    {
        private readonly IMemberService _memberService;
        private readonly IMembershipService _membershipService;
        private readonly IPlanService _planService;
        private readonly UserManager<IdentityUser> _userManager;


        public EditModel(IMembershipService membershipService,IMemberService memberService,IPlanService planService, UserManager<IdentityUser> userManager)
        {
            _membershipService = membershipService;
            _memberService = memberService;
            _planService = planService; 
            _userManager = userManager;
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var membership =  await _membershipService.GetById(id);
            if (membership == null)
            {
                return NotFound();
            }
            Membership = membership;
            var members = await _memberService.GetMembers();
            var plans = await _planService.GetPlans();
           ViewData["MemberId"] = new SelectList(members, "Id", "FirstName");
           ViewData["PlanId"] = new SelectList(plans, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
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
            await _membershipService.UpdateAsync(Membership.Id, Membership);

            return RedirectToPage("./Index");
        }

       
    }
}
