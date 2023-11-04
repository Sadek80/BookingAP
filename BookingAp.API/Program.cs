using BookingAp.API.Extensions;
using BookingAP.Application;
using BookingAP.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddHangifireExtension(configuration: builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //app.ApplyMigrations();
    //app.SeedData();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
