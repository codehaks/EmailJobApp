using WebApp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddHostedService<EmailWorker>();
//builder.Services.AddScoped<IBackgroundTaskQueue, BackgroundTaskQueue>();
//builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
//builder.Services.AddHostedService<QueuedHostedService>();
builder.Services.AddSingleton<ITaskJob, TaskJob>();
builder.Services.AddHostedService<EmailWorker>();
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
