using todo.Entities;
using todo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
var connectionString = builder.Configuration.GetConnectionString("Todo");
builder.Services.AddSqlServer<TodoDbContext>(connectionString);
builder.Services.AddScoped<ITodoService, TodoService>();
var  myAllowSpecificOrigins = "http://localhost:3000";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowReactApp",
        policy  =>
        {
            policy.WithOrigins(myAllowSpecificOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowReactApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();