using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddQuartz(q =>
{
    // Use a Scoped container to create jobs. I'll touch on this later
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Create a "key" for the job
    var jobKey = new JobKey("HelloWorldJob");

    // Register the job with the DI container
   
    
    q.AddJob<HelloWorldJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
                  .ForJob(jobKey) // link to the HelloWorldJob
                  .WithIdentity("HelloWorldJob-trigger") // give the trigger a unique name
                  .WithSimpleSchedule(x => x
            .WithIntervalInSeconds(5000)
            .RepeatForever()));
    //.WithCronSchedule("0/10 * * * * ?")) ; // run every 5 seconds
    //q.AddTriggerListener<HelloWorldJob>(opts =>opts.WithIdentity(jobKey));
});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
