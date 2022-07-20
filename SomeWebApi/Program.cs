using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis;
using SomeRepository;
using SomeService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IBooksService, BooksService>();


//MongoDb section
builder.Services.AddSingleton(x => builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>());
builder.Services.AddSingleton<IMongoDbRepository, MongoDbRepository>();

//redis
builder.Services.AddSingleton<IRedisClientsManager>(c => new RedisManagerPool(builder.Configuration.GetSection("Redis").Value));
builder.Services.AddSingleton<ICacheService, RedisCacheService>(); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books", async ([FromServices] IBooksService booksService) =>
{
    var books = await booksService.GetAsync();
    return books;
})
.WithName("GetBooks");
app.MapGet("/books/{id}", async ([FromServices] IBooksService booksService, [FromRoute] string id) =>
{
    var book = await booksService.GetAsync(id);
    return book;
})
.WithName("GetBook");
app.MapPost("/books", async ([FromServices] IBooksService booksService, [FromBody] DbBook book) =>
{
    await booksService.CreateAsync(book);
    return book;
})
.WithName("CreateBook");

app.Run();