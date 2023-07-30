using APP.Api.Errors;
using APP.Api.Extensions;
using APP.Api.Middleware;
using APP.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//-----------------------------------------------------API services configuration
builder.Services.AddApiRegestration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-----------------------------------------------------DataBase configuration
builder.Services.InfrastructureConfiguration(builder.Configuration);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//------------------------------------------------------Start Errors  Middlewares 

// 1- ..........................    My custom middleware
app.UseMiddleware<ExceptionMiddleware>();
// 2- ..........................  The builtin middleware
app.UseStatusCodePagesWithReExecute("/errors/{0}");

//--------------------------------------------------------End Errors  Middlewares 


app.UseHttpsRedirection();
app.UseStaticFiles();

//............................................................Enable Cors Servive
app.UseCors("CorsPolicy");

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();
InfrastructureRegistration.InfrastructureconfigMiddleware(app);
app.Run();
