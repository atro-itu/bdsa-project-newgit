var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GitContext>(o => o.UseNpgsql(builder.Configuration.GetConnectionString("Newgit")!));

builder.Services.AddScoped<IAnalysisRepository, AnalysisRepository>();
builder.Services.AddSingleton<ICommitFetcherService, CommitFetcherService>(service => CommitFetcherService.Instance);
builder.Services.AddSingleton<IForkFetcherService, ForkFetcherService>(service => ForkFetcherService.Instance);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
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
