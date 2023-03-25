using BlazorApp1.Data;
using BlazorApp1;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultScheme = "Cookies";
    opt.RequireAuthenticatedSignIn = false;
}).AddCookie(opt => opt.Cookie.Name = "Cookie");
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//        options =>
//        {
//            options.LoginPath = new PathString("/login");
//        });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//	_ = endpoints.MapControllers();
//	_ = endpoints.MapHub<OurHub>("/hub");
//	_ = endpoints.MapBlazorHub();
//	_ = endpoints.MapFallbackToPage("/_Host");
//});
app.MapControllers();
app.MapHub<OurHub>("/hub");
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapRazorPages();

app.Run();
