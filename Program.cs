using Microsoft.EntityFrameworkCore;
using ToDoList.API.Database.Context;
using ToDoList.API.Interfaces;
using ToDoList.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ToDoListContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ToDoListDB")));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IToDoListService, ToDoListService>();

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
