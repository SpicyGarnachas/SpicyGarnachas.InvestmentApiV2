using Microsoft.OpenApi.Models;
using SpicyGarnachas.InvestmentApiV2.Repositories.Interfaces;
using SpicyGarnachas.InvestmentApiV2.Repositories;
using SpicyGarnachas.InvestmentApiV2.Services.Interfaces;
using SpicyGarnachas.InvestmentApiV2.Services;
using Microsoft.Extensions.DependencyInjection;

namespace SpicyGarnachas.InvestmentApiV2;

public class Program
{

    private readonly IWebHostEnvironment webHostEnvironment;
    public IConfiguration Configuration { get; }
    public Program(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        Configuration = configuration;
        this.webHostEnvironment = webHostEnvironment;
    }

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddScoped<IPortfolioService, PortfolioService>();
        builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        builder.Services.AddScoped<IInvestmentService, InvestmentService>();
        builder.Services.AddScoped<IInvestmentRepository, InvestmentRepository>();
        builder.Services.AddScoped<ITransactionService, TransactionService>();
        builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
    }
}
