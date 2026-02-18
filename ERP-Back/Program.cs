using Container;
using Data.Connection;

var builder = WebApplication.CreateBuilder(args);

// -------------------------------------------------------------
// 1. CONFIGURAR EL SERVICIO DE CORS (Antes del Build)
// -------------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // <--- Tu Front-end aquí
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
// -------------------------------------------------------------


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

// -------------------------------------------------------------
// 2. ACTIVAR EL MIDDLEWARE CORS (Entre Routing y Authorization)
// -------------------------------------------------------------
app.UseCors("PermitirAngular");
// -------------------------------------------------------------

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();