using Data;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace GymMasterPro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ITrainerService _trainerService;
        private readonly ICheckinService _checkinService;
        private readonly IMemberService _memberService;

        public IndexModel(ILogger<IndexModel> logger, IMemberService memberService, ICheckinService checkinService, ITrainerService trainerService)
        {
            _logger = logger;
            _trainerService=trainerService;
            _memberService=memberService;
            _checkinService=checkinService;
        }
   
        public int TotalMember { get; set; } = default!;
         public int TotalTrainers { get; set; } = default!;
        public int ActiveMembers { get; set; } = default!;
        public int InActiveMembers { get; set; } = default!;
        public int? ExpiredMemberShip { get; set; } = default!;
        public IList<Member>? Members { get; set; } = default!;
        public async Task OnGet()
        {
            TotalMember = await _memberService.GetMembersCount();
            TotalTrainers = await _trainerService.GetTrainersCount();
            ActiveMembers = await _memberService.GetActiveMembers();
            ExpiredMemberShip = (await _memberService.GetExpiredMembers())?.Count();
            InActiveMembers = await _memberService.GetInactiveMembers();
            Members = (await _memberService.GetExpiredMembers())?.ToList();
        }
    }
}