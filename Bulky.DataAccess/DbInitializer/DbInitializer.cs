﻿
using Azure.Identity;
using BulkyBook.DataAccess.Data;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mono.TextTemplating;

namespace BulkyBook.DataAccess.DbInitializer;

public class DbInitializer : IDbInitializer
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _db = db;
    }

    public void Initialize()
    {
        try
        {
            if(_db.Database.GetPendingMigrations().Count() > 0) 
            {
                _db.Database.Migrate();
            }
        }
        catch (Exception ex)
        {

        }

        _db.Database.EnsureCreated();

        if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult())
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();


            //if roles are not created, then we will create admin user as well
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@test.mail",
                Email = "admin@test.mail",
                Name = "Admin Start",
                PhoneNumber = "1112223333",
                StreetAdress = "test 123",
                State = "Sverige",
                PostalCode = "23422",
                City = "Osby"
            }, "Admin123*").GetAwaiter().GetResult();


            ApplicationUser user = _db.applicationUsers.FirstOrDefault(u => u.Email == "admin@test.mail");
            _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();

        }

        return;
    }
}
