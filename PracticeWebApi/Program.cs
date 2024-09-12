using Microsoft.AspNetCore.Mvc.Formatters;
using PracticeWebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddControllers(options =>
{
    // options.ReturnHttpNotAcceptable = true; // Return 406 Not Acceptable if no formatter is found
    options.OutputFormatters.Add(new XmlSerializerOutputFormatter()); // Add support for XML
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddJwtBearerSwaggerGen();  // custom extension method
});
builder.Services.AddCors();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("connString"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(option => option.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();