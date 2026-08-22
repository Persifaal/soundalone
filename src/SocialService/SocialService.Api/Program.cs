using Microsoft.AspNetCore.Server.Kestrel.Core;
using SocialService.Api.Grpc;
using SocialService.Application;
using SocialService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<UserProfileGrpcService>(options =>
{
    options.Address = new Uri(
        builder.Configuration["Grpc:SocialService"]!
    );
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();
builder.Services.AddGrpcReflection();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(8080, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapGrpcReflectionService();
}


//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapGrpcService<UserProfileGrpcService>();
app.MapControllers();

app.Run();
