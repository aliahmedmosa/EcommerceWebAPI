using APP.Infrastructure;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//-----------------------------------------------------DataBase configuration
builder.Services.InfrastructureConfiguration(builder.Configuration);

//-----------------------------------------------------auto mapper configuration
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//-----------------------------------------------------IFileProvider configuration
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
    Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//------------------------------------------------------Errors  Middleware 
app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
