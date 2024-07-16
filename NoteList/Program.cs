using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteList.DomainLayer.Data;
using NoteList.RepositoryLayer.IRepositories;
using NoteList.RepositoryLayer.Repositories;
using NoteList.ServiceLayer.IServices;
using NoteList.ServiceLayer.MappingProfiles;
using NoteList.ServiceLayer.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 4;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("DeleteNotePolicy", policy => policy.RequireClaim("Delete Note"));
    options.AddPolicy("EditNotePolicy", policy => policy.RequireClaim("Edit Note"));
    options.AddPolicy("CreateNotePolicy", policy => policy.RequireClaim("Create Note"));
    options.AddPolicy("ViewNotePolicy", policy => policy.RequireClaim("View Note"));

    options.AddPolicy("DeleteTodoListPolicy", policy => policy.RequireClaim("Delete TodoList"));
    options.AddPolicy("EditTodoListPolicy", policy => policy.RequireClaim("Edit TodoList"));
    options.AddPolicy("CreateTodoListPolicy", policy => policy.RequireClaim("Create TodoList"));
    options.AddPolicy("ViewTodoListPolicy", policy => policy.RequireClaim("View TodoList"));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";

    options.AccessDeniedPath = "/Home/AccessDenied";
});

builder.Services.AddAutoMapper(typeof(Mappings));

builder.Services.AddScoped<INoteRepository, NoteRepository>();
builder.Services.AddScoped<ITodoListRepository, TodoListRepository>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IClaimService, ClaimService>();
builder.Services.AddScoped<IAdministrationService, AdministrationService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITodoListService, TodoListService>();

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
