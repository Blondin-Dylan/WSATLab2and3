using Microsoft.EntityFrameworkCore;
using TodoApi.Data;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers().AddXmlSerializerFormatters();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll",
    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});



builder.Services.AddDbContextFactory<TodoContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("TodoConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();
app.Run();
