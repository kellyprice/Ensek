using AutoMapper;
using DAL;
using Ensek;
using Ensek.Data;
using Ensek.Library;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration.GetConnectionString("EnsekConn");
var context = builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(connection));
// Add services to the container.

builder.Services.AddControllersWithViews();

//Library
builder.Services.AddTransient<AccountHandler>();
builder.Services.AddTransient<DuplicateHandler>();
builder.Services.AddTransient<FileDuplicateHandler>();
builder.Services.AddTransient<MeterReading>();
builder.Services.AddTransient<MeterReadingDateHandler>();
builder.Services.AddTransient<MeterReadingHandler>();
builder.Services.AddTransient<Validate>();

//Data
builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<IMeterReadingRepository, MeterReadingRepository>();

//DTOs
builder.Services.AddTransient<IRepository<AccountDTO>, Repository<AccountDTO>>();
builder.Services.AddTransient<IRepository<MeterReadingDTO>, Repository<MeterReadingDTO>>();

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MapperConfig());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // reset Ensek database    
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var accountRepository = scope.ServiceProvider.GetRequiredService<IRepository<AccountDTO>>();

    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    var resetData = new ResetData(accountRepository);

    resetData.CreateAccountData();
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
