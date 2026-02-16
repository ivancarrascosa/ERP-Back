using Container;
using Data.Connection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Primero registrar la conexión
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddSingleton(new Conexion(connectionString));

// Después los servicios que dependen de ella
builder.Services.AddContainer(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();