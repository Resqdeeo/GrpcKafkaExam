using GrpcServer.Kafka;
using GrpcServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddSingleton<KafkaProducer>();

builder.Services.Configure<KafkaProducer>(builder.Configuration.GetSection("Kafka"));


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<ItemServiceImpl>();
app.MapGet("/", () => "Use gRPC client.");

var kafkaAdmin = app.Services.GetRequiredService<KafkaProducer>();
await kafkaAdmin.CreateTopicIfNotExistsAsync("item-topic");

app.Run();
