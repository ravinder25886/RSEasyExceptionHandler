//using RS_EasyExceptionHandling;

using RS_EasyExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRS_ErrorHandlingMiddleware();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 
app.UseAuthorization();

app.MapControllers();

RSDependencyInjection.SetupLogDataBase(app.Services.CreateScope());
app.UseMiddleware<RS_ErrorHandlingMiddleware>();

app.Run();
