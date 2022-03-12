using Hangfire;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
