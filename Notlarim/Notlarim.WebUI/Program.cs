using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Notlarim.Business.Abstract;
using Notlarim.Business.Concrete;
using Notlarim.Data.Abstract;
using Notlarim.Data.Concrete;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NoteContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnectionStrings")));

builder.Services.AddScoped<INoteRepository, EfNoteRepository>();
builder.Services.AddScoped<ICategoryRepository, EfCategoryRepository>();
builder.Services.AddScoped<IMemberRepository, EfMemberRepository>();
builder.Services.AddScoped<IMessageRepository, EfMessageRepository>();


builder.Services.AddScoped<INoteService, NoteManager>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IMemberService, MemberManager>();
builder.Services.AddScoped<IMessageService, MessageManager>();

builder.Services.AddSession();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddMvc();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.Cookie.Name = "test";
        x.LoginPath = "/Login/LoginFromRestApi";
        x.AccessDeniedPath = "/Login/LoginFromRestApi";
    }
    );

if (builder.Environment.IsDevelopment())
{
    SeedDatabase.Seed();
}

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=GetNoteFromRestApi}/{id?}");

app.Run();
