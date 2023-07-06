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

namespace GymMasterPro.Pages.Plans
{
    public class IndexModel : PageModel
    {
        private readonly IPlanService _planService;

        public IndexModel(IPlanService planService)
        {
            _planService = planService;
        }

        public IList<Plan> Plan { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Plan = await _planService.GetPlans();
        }
    }
}
