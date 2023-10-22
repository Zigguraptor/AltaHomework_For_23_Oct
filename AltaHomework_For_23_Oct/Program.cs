using AltaHomework_For_23_Oct;
using AltaHomework_For_23_Oct.DAL;
using AltaHomework_For_23_Oct.DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// mapping
builder.Services.AddAutoMappingProfiles();

// db
builder.Services.AddDbContext<UsersDataDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("UsersDataDb")));
builder.Services.AddScoped<UnitOfWork>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// swagger
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
