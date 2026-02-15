


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure the connection string for the data access layer
string? connectingString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectingString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
Inventory.DataAccess.clsDataAccessSettings.ConnectionString = connectingString;

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
