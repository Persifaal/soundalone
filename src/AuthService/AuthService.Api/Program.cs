using AuthService.Application;
using AuthService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGrpcReflectionService();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGrpcService<AuthService.Api.Grpc.AuthGrpcService>();
app.MapControllers();

app.Run();
