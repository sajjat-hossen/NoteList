using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteList.DomainLayer.Data;
using NoteList.RepositoryLayer.IRepositories;
using NoteList.RepositoryLayer.Repositories;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 4;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DeleteRolePolicy", policy => policy.RequireClaim("Delete Role"));
    options.AddPolicy("EditRolePolicy", policy => policy.RequireClaim("Edit Role"));
    options.AddPolicy("CreateRolePolicy", policy => policy.RequireClaim("Create Role"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath = "/Home/AccessDenied";
});

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAdministrationService, AdministrationService>();

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
