using Duende.IdentityServer.Services;
using FatecSisMed.IdentityServer.Configuration;
using FatecSisMed.IdentityServer.Data.Context;
using FatecSisMed.IdentityServer.Data.Entities;
using FatecSisMed.IdentityServer.SeedDataBase.Entities;
using FatecSisMed.IdentityServer.Services.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext> (options =>
options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var builderIdentityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}
).AddInMemoryIdentityResources(
    IdentityConfiguration.IdentityResources)
.AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
.AddInMemoryClients(IdentityConfiguration.Clients)
.AddAspNetIdentity<ApplicationUser>();

builderIdentityServer.AddDeveloperSigningCredential();

builder.Services.AddScoped<IDatabaseInitializer,
    DataBaseIdentityServerInitializer>();

builder.Services.AddScoped<IProfileService, ProfileAppService>();

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

app.UseIdentityServer();

app.UseAuthorization();

SeedDatabaseIdentityServer(app);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{faction=Index}/{id?}");

app.Run();

void SeedDatabaseIdentityServer(IApplicationBuilder app)
{
    using (var serviceScope = app.ApplicationServices.CreateScope())
    {
        var initRoleUsers = serviceScope.ServiceProvider
            .GetService<IDatabaseInitializer>();

        initRoleUsers.InitializeSeedRoles();
        initRoleUsers.InitializeSeedUsers();
    }
}

app.MapRazorPages();

app.Run();
