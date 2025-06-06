using AI_service.Feature.TrainModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddMessage();
builder.Services.AddDbConnection(builder.Configuration);
builder.Services.AddTrainModelFeature();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();
builder.Services.AddEndpoints();

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.ApplyMigration();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.Run();
