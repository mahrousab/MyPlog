using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyOwnPlog.Web.Data;
using MyOwnPlog.Web.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var ConnectionString = builder.Configuration.GetConnectionString("DefualtConnection") ??
	throw new InvalidOperationException("now connection string was found");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(ConnectionString));

builder.Services.AddDbContext<AuthDbContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
	AddEntityFrameworkStores<AuthDbContext>();


builder.Services.Configure<IdentityOptions>(options =>
{

	options.Password.RequireDigit = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequiredLength = 6;
	options.Password.RequiredUniqueChars = 1;

}
);

builder.Services.AddScoped<ITagRepository,  TagRepository>();

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();

builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

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
