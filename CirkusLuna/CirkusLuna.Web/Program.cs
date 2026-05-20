using CirkusLuna.Core.Interfaces;
using CirkusLuna.Core.Repositories;
using CirkusLuna.Core.Services;
using CirkusLuna.Core.SeedData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Register repositories as singletons (in-memory data persists for app lifetime)
builder.Services.AddSingleton<IByRepository, InMemoryByRepository>();
builder.Services.AddSingleton<IArtistRepository, InMemoryArtistRepository>();
builder.Services.AddSingleton<IKundeRepository, InMemoryKundeRepository>();
builder.Services.AddSingleton<IForestillingRepository, InMemoryForestillingRepository>();
builder.Services.AddSingleton<IReservationRepository, InMemoryReservationRepository>();

// Register services
builder.Services.AddScoped<IForestillingService, ForestillingService>();
builder.Services.AddScoped<IReservationService, ReservationService>();

var app = builder.Build();

// Seed initial data
using (var scope = app.Services.CreateScope())
{
    var byRepo = scope.ServiceProvider.GetRequiredService<IByRepository>();
    var artistRepo = scope.ServiceProvider.GetRequiredService<IArtistRepository>();
    var kundeRepo = scope.ServiceProvider.GetRequiredService<IKundeRepository>();
    var forestillingRepo = scope.ServiceProvider.GetRequiredService<IForestillingRepository>();
    
    DataSeeder.SeedAll(byRepo, artistRepo, kundeRepo, forestillingRepo);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
