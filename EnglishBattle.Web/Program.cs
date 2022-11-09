using EnglishBattle.DAL;
using EnglishBattle.BLL;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services
    .AddDataAccessLayer()
    .AddBusinessLayer();

//builder.Services.AddLocalization();

var app = builder.Build();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

await EnglishBattleContextSeed.SeedAsync(services.GetRequiredService<EnglishBattleContext>());

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
