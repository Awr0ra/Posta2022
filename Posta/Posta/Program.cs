using Hangfire;
using FluentValidation;
using FluentValidation.AspNetCore;
using PostaTypes.Helpers.Validation;
using PostaTypes.Contracts.Validators;
using PostaTypes.Contracts.Requests.Validation;

var builder = WebApplication.CreateBuilder(args);

// add configure JSON logging to the console
//builder.Logging.AddJsonConsole();
builder.Logging.AddConsole();

#region add source of appsettings.<environment>.json

builder.Configuration
  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
  .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);


var useAppSettings = builder.Configuration.GetSection("GlobalOptions:Proxy");
Console.WriteLine($"<<< {builder.Environment.ApplicationName} >>> {DateTime.Now:yyyy-MM-dd HH:mm:ss} / Environment:'{builder.Environment.EnvironmentName}'; Global proxy: '{useAppSettings.GetValue<string>("Url")}'; Proxy using:'{useAppSettings.GetValue<bool>("UseProxy")}'");

#endregion

// Add services to the container.
//builder.Services.AddControllers();

// Add services to the container.
// with FluentValidation custom error response
builder.Services.AddControllers()
    .AddFluentValidation()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = CustomValidationFailureHelper.MakeValidationResponse;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Validators
builder.Services.AddScoped<IValidator<ValidationCheckRequest>, ValidationCheckRequestValidator>();

#endregion

#region HangFire

var dbConnectionStringHangfire = builder.Configuration.GetConnectionString("HangfireDB");
if (String.IsNullOrEmpty(dbConnectionStringHangfire))
    throw new ArgumentNullException("ConnectionStrings[HangfireDB]", "Hangfire's DB connection string is not set");

builder.Services.AddHangfire(configuration =>
{
    configuration.UseSqlServerStorage(dbConnectionStringHangfire);
});
builder.Services.AddHangfireServer();

#endregion


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard("/hfdash"); //add HangFire Dashboard

app.MapControllers();

app.Run();
