using FatecSisMed.IdentityServer.Configuration;
using FatecSisMed.IdentityServer.Data.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FatecSisMed.IdentityServer.SeedDataBase.Entities
{
    public class DataBaseIdentityServerInitializer : IDatabaseInitializer
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DataBaseIdentityServerInitializer(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }
        public void InitializeSeedRoles()
        {
            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Admin).Result)
            {
                IdentityRole roleAdmin = new IdentityRole();
                roleAdmin.Name = IdentityConfiguration.Admin;
                roleAdmin.NormalizedName = IdentityConfiguration.Admin.ToUpper();
                _roleManager.CreateAsync(roleAdmin).Wait();
            }

            if (!_roleManager.RoleExistsAsync(IdentityConfiguration.Client).Result)
            {
                IdentityRole roleClient = new IdentityRole();
                roleClient.Name = IdentityConfiguration.Client;
                roleClient.NormalizedName = IdentityConfiguration.Client.ToUpper();
                _roleManager.CreateAsync(roleClient).Wait();

            }
        }
        public void InitializeSeedUsers()
        {
            if (_userManager.FindByEmailAsync("tamiris@gmail.com").Result is null)
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "tamiris",
                    NormalizedUserName = "TAMIRIS",
                    Email = "tamiris@gmail.com",
                    NormalizedEmail = "EMERSON@GMAIL.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55(17)997197663",
                    FirstName = "Usuario",
                    LastName = "Tamiris",
                    SecurityStamp = Guid.NewGuid().ToString(),
                };
                IdentityResult resultAdmin = _userManager.CreateAsync(admin, "Admin@1234").Result;

                if (resultAdmin.Succeeded)
                {
                    _userManager.AddToRoleAsync(admin, IdentityConfiguration.Admin).Wait();

                    var adminClaims = _userManager.AddClaimsAsync(admin, new Claim[]
                    {
                        new Claim(JwtClaimTypes.Name, $"{admin.FirstName}:{admin.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                        new Claim (JwtClaimTypes.Role, IdentityConfiguration.Admin)


                    }).Result;
                }
            }

            if (_userManager.FindByEmailAsync("client@gmail.com").Result is null)
            {
                ApplicationUser client = new ApplicationUser()
                {
                    UserName = "emersonclient",
                    NormalizedUserName = "EMERSONCLIENT",
                    Email = "client@gmail.com",
                    NormalizedEmail = "CLIENT@GMAIL.COM",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    PhoneNumber = "+55 (11) 99012-3456",
                    FirstName = "Usuario",
                    LastName = "Client",
                    SecurityStamp = Guid.NewGuid().ToString()
                };

                IdentityResult resultClient = _userManager.CreateAsync(client, "Client@1234").Result;
                if (resultClient.Succeeded)
                {
                    _userManager.AddToRoleAsync(client, IdentityConfiguration.Client).Wait();

                    var clientClaims = _userManager.AddClaimsAsync(client, new Claim[] {
                        new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                        new Claim(JwtClaimTypes.GivenName, client.FirstName),
                        new Claim(JwtClaimTypes.FamilyName, client.LastName),
                        new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
                    }).Result;
                }
            }

        }
    }
}
