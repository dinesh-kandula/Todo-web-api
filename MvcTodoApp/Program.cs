using Microsoft.EntityFrameworkCore;
using MvcTodoApp.Models;
using MvcTodoApp.Services;
using TodoModels.Models;

var builder = WebApplication.CreateBuilder(args);

// Add framework services.
builder.Services
	.AddControllersWithViews();
	builder.Services.AddMvc().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Add Kendo UI services to the services container
builder.Services.AddKendo();


// Add services to the container.
builder.Services.AddHttpClient<IHttpRequestService, HttpRequestService>(c =>
{
    c.BaseAddress = new Uri("http://localhost:5113/");
});

builder.Services.AddDbContext<MvcDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")
            ?? throw new InvalidOperationException("Connection string 'TodoDbContext' not found.")));

builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddScoped <IWebRequestService, WebRequestService>();

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

app.UseCors();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blogs}/{action=Index}/{id?}");

app.Run();
