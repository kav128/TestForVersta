using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using TestForVersta;
using TestForVersta.BLL;
using TestForVersta.DAL;
using TestForVersta.Models;
using TestForVersta.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
       .UseDataAccessLayer(optionsBuilder => optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("sqlite-db")))
       .UseBusinessLogicLayer()
       .AddAutoMapper(expression => expression.AddProfilesFromBLL()
                                              .AddProfilesFromPresentation())
       .AddScoped<IValidator<OrderInsertModel>, OrderInsertModelValidator>()
       .AddControllersWithViews();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var applicationContext = scope.ServiceProvider.GetService<ApplicationContext>();
    if (applicationContext != null)
    {
        await applicationContext.Database.EnsureDeletedAsync();
        await applicationContext.Database.EnsureCreatedAsync();
        // TODO await applicationContext.Database.MigrateAsync();
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

var supportedCultures = new[]
{
    new CultureInfo("en-US")
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
