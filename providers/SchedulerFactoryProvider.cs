using onboarding_dotnet.Infrastructures.Schedulers;
using Quartz;

namespace onboarding_dotnet.Providers
{
    public partial class SchedulerFactoryProvider(WebApplication application)
    {
        private readonly WebApplication app = application;

        public async Task InitSchedulerFactory()
        {
            var schedulerFactory = app.Services.GetRequiredService<ISchedulerFactory>();
            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            var jobDetail = JobBuilder.Create<OrderAutoCancelJob>()
                .WithIdentity("orderAutoCancelJob")
                .Build();

            // Run the job every 1 minute
            var trigger = TriggerBuilder.Create()
                .WithIdentity("orderAutoCancelJobTrigger")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                .Build();

            await scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}