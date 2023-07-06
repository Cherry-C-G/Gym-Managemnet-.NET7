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
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace GymMasterPro.Pages.Checkins
{
    public class CreateModel : PageModel
    {
        private readonly ICheckinService _checkinService;
        private readonly IMemberService _memberService;
        private readonly UserManager<IdentityUser> _userManager;


        public CreateModel(ICheckinService checkinService, IMemberService memberService, UserManager<IdentityUser> userManager)
        {
            _checkinService = checkinService;
            _memberService = memberService;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            GetMembers();
            return Page();
        }

        private async void GetMembers()
        {
            var members = await _memberService.GetMembers();
            ViewData["MemberId"] = new SelectList(members, "Id", "FirstName");
        }

        [BindProperty]
        public Checkin Checkin { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || Checkin == null)
            {
                return Page();
            }
            //var loggedInUser = await _userManager.GetUserAsync(User);
            //if (loggedInUser == null)
            //{
            //    return Page();
            //}
            if(await _memberService.CheckIfExpired(Checkin.MemberId))
            {
                GetMembers();
                 ViewData["Message"] = "Membership is expired for this member.";
                    return Page();
            }
            if(await _checkinService.AlreadyCheckedIn(Checkin.MemberId))
            {
                GetMembers();
                ViewData["Message"] = "You have already checked in.";
                return Page();
            }
            Checkin.UpdateAt = DateTime.Now;
            Checkin.CreatedAt = DateTime.Now;
           // Checkin.CreatedBy = loggedInUser?.UserName;

          
            await _checkinService.SaveAsync(Checkin);

            return RedirectToPage("./Index");
        }
    }
}
