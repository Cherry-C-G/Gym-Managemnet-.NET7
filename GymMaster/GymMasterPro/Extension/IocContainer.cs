
using Repository;
using Services;
using Services.Interfaces;

namespace GymMasterPro.Extension
{
    public static class IocContainer
    {
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<ICheckinService, CheckinService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IMembershipService, MembershipService>();
        }
    }
}
