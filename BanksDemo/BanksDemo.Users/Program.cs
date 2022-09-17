using BanksDemo.User.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigurationForDb(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMassTransitRabbitMq(builder.Configuration);
 builder.Services.AddBusinessRules();
builder.Services.AddHttpClientByConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
