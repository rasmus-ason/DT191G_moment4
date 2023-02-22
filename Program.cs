using DT191G_moment4.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SongContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("SongDbString"))
);

builder.Services.AddDbContext<ArtistContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("ArtistDbString"))
);

builder.Services.AddDbContext<RatingContext>(options => 
    options.UseSqlite(builder.Configuration.GetConnectionString("RatingDbString"))
);





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
