using Microsoft.EntityFrameworkCore;
using TodoApi;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5002");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseMySql("server=localhost;user=root;password=1234;database=ToDoDB",
     Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql")));

var app = builder.Build();
app.UseCors("AllowAllOrigins");

// Swagger configuration
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}

app.MapGet("/", async (ToDoDbContext dbContext) =>//to get all the tasks
{
    var items = await dbContext.Items.ToListAsync();
    return Results.Ok(items);
});

app.MapPost("/", async (Item task, ToDoDbContext dbContext) =>
{//adding a task
    dbContext.Items.Add(task);
    await dbContext.SaveChangesAsync();
    return Results.Created($"/{task.Id}", task);
});

app.MapPut("/{id}", async (int id, Item updatedTask, ToDoDbContext dbContext) =>
{
    var existingTask = await dbContext.Items.FindAsync(id);
    if (existingTask is null)
    {
        return Results.NotFound();
    }
    if (!string.IsNullOrEmpty(updatedTask.Name))
    {
        existingTask.Name = updatedTask.Name;
    }
    existingTask.IsComplete = updatedTask.IsComplete;
    await dbContext.SaveChangesAsync();
    return Results.Ok(existingTask);
});


app.MapDelete("/{id}", async (int id, ToDoDbContext dbContext) =>
{
    var taskToDelete = await dbContext.Items.FindAsync(id);
    if (taskToDelete is null)
    {
        return Results.NotFound();
    }

    dbContext.Items.Remove(taskToDelete);
    await dbContext.SaveChangesAsync();
    return Results.NoContent();
});



app.Run();
public class Item
{
    public Item() { }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsComplete { get; set; }
}
