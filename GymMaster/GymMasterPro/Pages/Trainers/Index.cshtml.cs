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
    public class IndexModel : PageModel
    {
        private readonly ITrainerService _trainerService;

        public IndexModel(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        public IList<Trainer> Trainer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Trainer = await _trainerService.GetTrainers();
        }
    }
}
