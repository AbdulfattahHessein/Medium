using Medium.BL;
using Medium.BL.Middlewares;
using Medium.DA;
using Medium.DA.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(conf =>
{
    // diasble validation on controller
    conf.ModelValidatorProviders.Clear();
});
// diasble validation on controller
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDataAccessDependencies(builder.Configuration);

//builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
//                    .AddEntityFrameworkStores<ApplicationDbContext>()
//                    .AddUserValidator<CustomUserValidator<ApplicationUser>>()
//                    .AddPasswordValidator<CustomPasswordValidator<ApplicationUser>>();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Name = "Bearer",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new List<string>()
        }
    });

});

builder.Services.AddBusinessLogicDependencies();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationDbContext>();

    context.Database.Migrate();

}


app.UseMiddleware<ExceptionHandlerMiddleware>();
//app.UseMiddleware<UserExistenceMiddleware>();


app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => options.EnableTryItOutByDefault());
}

app.UseHttpsRedirection();

// Add authentication middleware.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
