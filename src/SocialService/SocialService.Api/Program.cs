using Microsoft.AspNetCore.Server.Kestrel.Core;
using SocialService.Api.Grpc;
using SocialService.Application;
using SocialService.Infrastructure;

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
}
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGrpcService<UserProfileGrpcService>();
app.MapControllers();

app.Run();
