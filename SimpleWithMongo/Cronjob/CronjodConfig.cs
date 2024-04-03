using Quartz;
namespace SimpleWithMongo.Cronjob;

public class CronjodConfig
{
    public static void ConfigCronjod(IServiceCollection services)
    {
        services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
        services.AddQuartz(opt =>
        {
            opt.SchedulerId = "MainScheduler";

            opt.UseSimpleTypeLoader();
            opt.UseInMemoryStore();
            opt.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 10; });

            var jobKey = new JobKey("Get Data", "Default Group");
            opt.AddJob<JodGetData>(jobKey,
                j => j.WithDescription(
                    "Get Data form Api"));

            opt.AddTrigger(t => t
                .WithIdentity("7days")
                .ForJob(jobKey)
                .StartNow()
                .WithDailyTimeIntervalSchedule(s => s
                .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(12, 0))
                .WithIntervalInHours(24) 
    )
                .WithDescription("Trigger at 12 AM every day"));
        });
        services.AddTransient<JodGetData>();
    }
}
