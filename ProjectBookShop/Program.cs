using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using ProjectBookShop.classes;
using ProjectBookShop.Models;
using ProjectBookShop.Models.Repository;
using ProjectBookShop.Models.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using ProjectBookShop.Data;
using ProjectBookShop.Areas.Identity.Data;
using System;
using Microsoft.AspNetCore.Mvc.Versioning;

using ProjectBookShop.Areas.Identity.Sercices;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("BloggingDatabase");
builder.Services.AddDbContext<BookShopContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));

var connectionStringIdentity = builder.Configuration.GetConnectionString("IdentityDBContextConnection");
builder.Services.AddDbContext<IdentityDBContext>(option => option.UseSqlServer(connectionStringIdentity));
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<ApplicationRole>().AddEntityFrameworkStores<IdentityDBContext>().AddErrorDescriber<ApplicationIdentityErrorDescriber>();
builder.Services.Configure<IdentityOptions>(options => {
    //confrigure Password
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
    
});
builder.Services.AddApiVersioning(options =>
{
//اگر بخواهیم ورژن به هدر درخواست اضافه شود  
options.ReportApiVersions = true;
//زمانی که اکشن متد ورژنی براش تعریف نشده یک ورژن پیش فرض میگیرد دیگه خطا گرفته نمی شود 
options.AssumeDefaultVersionWhenUnspecified = true;
options.DefaultApiVersion = new ApiVersion(1, 0);
//options.ApiVersionReader = new HeaderApiVersionReader("api-version");
options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(),new HeaderApiVersionReader("api-version"));
});
//builder.Services.Configure<ApiBehaviorOptions>(optins =>
//{
//    optins.InvalidModelStateResponseFactory = actioncontext =>
//    {
//        var errors = actioncontext.ModelState
//        .Where(e => e.Value.Errors.Count() != 0)
//        .Select(e => e.Value.Errors.First().ErrorMessage).ToList();
//        return new BadRequestObjectResult(errors);
//    };
//});



builder.Services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
builder.Services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
builder.Services.AddScoped<ApplicationIdentityErrorDescriber>();
//builder.Services.AddScoped<ConvertMladiToShamsi>();
builder.Services.AddScoped<IConvertDate,ConvertDate>();
builder.Services.AddScoped<ConvertDate>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BookShopContext>();
builder.Services.AddScoped<BookRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
builder.Services.AddMvc(options =>
{
    var F = builder.Services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
    var L = F.Create("ModelBindingMessages","ProjectBookShop");
    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
     (x) => L["انتخاب یکی از موارد لیست الزامی است."]);

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
   
   
    app.UseHsts();
    

}
app.UseFileServer(new FileServerOptions()
{
    FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
    RequestPath = new PathString("/node_modules"),
    EnableDirectoryBrowsing = true
});
//app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "Admin",
    pattern: "{area=Admin}/{controller=Book}/{action=Index}/{id?}"
  );


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//    );


app.MapRazorPages();

app.Run();
