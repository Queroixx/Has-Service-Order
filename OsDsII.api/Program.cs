using Microsoft.EntityFrameworkCore;
using OsDsII.api.Data;
using OsDsII.api.Repository.CommentsRepository;
using OsDsII.api.Repository.CustomersRepository;
using OsDsII.api.Repository.ServiceOrderRepository;
using OsDsII.api.Services.Customers;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection"));
});
// Add services to the container.

builder.Services.AddCors();


builder.Services.AddScoped<IServiceOrderRepository, ServiceOrderRepository>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<ICommentsRepository, CommentsRepository>();

builder.Services.AddScoped<ICustomersService, CustomersService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
