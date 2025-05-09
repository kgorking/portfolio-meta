using Microsoft.EntityFrameworkCore;
using portfolio.DataContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddDbContext<PortfolioContext>(opt => opt.UseInMemoryDatabase("TempDb"));
builder.Services.AddDbContext<PortfolioContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<PortfolioContext>();

if (await context.TestConnectionAsync())
{
    Console.WriteLine("Database connection successful.");
}
else
{
    Console.WriteLine("Database connection failed.");
}

app.Run();
