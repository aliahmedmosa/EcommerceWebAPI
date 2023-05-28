using APP.Api.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace APP.Api.Extensions
{
    public static class ApiRegestration
    {
        //------------This class to Register the services Except making it in the program.cs 
        public static IServiceCollection AddApiRegestration(this IServiceCollection services)
        {
            //-----------------------------------------------------auto mapper configuration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            //-----------------------------------------------------IFileProvider configuration
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            //.........Change for behavior for api controller with ApiValidationErrorResponse Error 400 Error
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = context.ModelState
                                        .Where(x => x.Value.Errors.Count > 0)
                                        .SelectMany(x => x.Value.Errors)
                                        .Select(x => x.ErrorMessage).ToArray()
                    };
                    return new BadRequestObjectResult(errorResponse);
                };

            });

            //...........................................................................Enable CORS Configurations
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", pol =>
                {
                    pol.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200");
                });
            });
            return services;
        }
    }
}
