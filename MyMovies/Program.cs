global using System.ComponentModel.DataAnnotations;
global using Microsoft.EntityFrameworkCore;
global using MyMovies.Models;
global using MyMovies.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Newtonsoft.Json;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(conectionString, ServerVersion.AutoDetect(conectionString));
});


builder.Services.AddIdentity<User, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 4;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    }
    ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders().AddRoles<IdentityRole>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var role = "Admin"; //use array to add more roles

    //use foreach(roles in roles to check and add)
    if(!await roleManager.RoleExistsAsync(role)) await roleManager.CreateAsync(new IdentityRole(role));

    SeedData.Initialize(services);
}
using(var scope = app.Services.CreateScope())
{
    string Email = "admin@admin.com";
    var password = "1234";


    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    if (await userManager.FindByEmailAsync(Email) == null)
    {
        var adminUser = new User();
        adminUser.Name = "Admin";
        adminUser.UserName = Email;
        adminUser.Email = Email;

        await userManager.CreateAsync(adminUser, password);
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

}



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


using (StreamReader r = new StreamReader("C:\\Users\\intern.pmquang1\\C#\\MyMovies\\MyMovies\\wwwroot\\movies-250.json"))
{
    string json = r.ReadToEnd();
    MovieTestArray movieTest = JsonConvert.DeserializeObject<MovieTestArray>(json);
    
}

app.Run();
