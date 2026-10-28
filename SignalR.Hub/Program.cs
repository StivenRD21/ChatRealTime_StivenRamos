using SignalR.HubServer.Hubs;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() 
                     ?? new[] { "https://localhost:7002" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("ClientPolicy");

app.MapHub<ChatHub>("/chatHub");

app.Run();