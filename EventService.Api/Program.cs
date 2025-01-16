using EventManager.FileRepository.Extensions;
using EventManager.FileRepository.Models;
using EventManager.Service.Extensions;



var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorageOptions"));

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
