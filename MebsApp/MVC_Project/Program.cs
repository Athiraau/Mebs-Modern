using Microsoft.Build.Execution;
using MVC_Project.DTO;
using MVC_Project.Repository;

var builder = WebApplication.CreateBuilder(args);
var mvcBuilder = builder.Services.AddRazorPages();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}




// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSession();
builder.Services.AddTransient<DecryptionRepo>();
builder.Services.AddTransient<GetDataRepo>();
builder.Services.AddTransient<Util>();
builder.Services.AddTransient<PostDataRepo>();
builder.Services.AddTransient<empDto>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseSession();
//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
//pattern: "{controller=Data}/{action=Dashboard}/{id?}");
pattern: "{controller=Loans}/{action=LoginPage}/{id?}");
//pattern: "{controller=Data}/{action=ex}/{id?}");

app.Run();
