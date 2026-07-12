using Microsoft.EntityFrameworkCore;
using MINI_DGPAY.DataHub.Models;
using MINI_DGPAY.Services.Main;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDBContext>(
    opt =>{
        opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
    }
);

builder.Services.AddScoped<CommonResponse<BtAccount>>();
builder.Services.AddScoped<CommonResponse<BtTransaction>>();
builder.Services.AddScoped<TranServices>();
builder.Services.AddScoped<AccServices>();
builder.Services.AddScoped<ResultModel>();
builder.Services.AddScoped<TranServices>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
