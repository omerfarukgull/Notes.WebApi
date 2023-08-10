using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Notlarim.Business.Abstract;
using Notlarim.Business.Concrete;
using Notlarim.Data.Abstract;
using Notlarim.Data.Concrete;

namespace Notlarim.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            });


            builder.Services.AddDbContext<NoteContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MssqlConnectionStrings")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
                    x.LoginPath = "/Login/Login";
                    x.AccessDeniedPath = "/Login/Login";
                }
                );


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}