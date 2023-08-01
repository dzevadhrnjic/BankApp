using BankApp.Accounts.Data;
using BankApp.Accounts.Services;
using BankApp.Analytics.Services;
using BankApp.Blacklist.Data;
using BankApp.Blacklist.Services;
using BankApp.ChangePassword.Services;
using BankApp.EmailVerification.Data;
using BankApp.EmailVerification.Services;
using BankApp.PdfFile.Data;
using BankApp.PdfFile.Services;
using BankApp.Transactions.Data;
using BankApp.Transactions.Services;
using BankApp.Transactions.Validations;
using BankApp.Users.Data;
using BankApp.Users.Services;
using BankApp.Users.Utils;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.d

builder.Services.AddDbContext<UserDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddDbContext<AccountDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddDbContext<TransactionDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddDbContext<VerificationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});


builder.Services.AddDbContext<BlacklistDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddDbContext<StatementDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at httsps://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<IAmountService, AmountService>();
builder.Services.AddScoped<ITransactionValidationService, TransactionValidationService>();
builder.Services.AddScoped<IVerificationService, VerificationService>();
builder.Services.AddScoped<IChangePasswordService, ChangePassword>();
builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
builder.Services.AddScoped<IBlacklistService, BlacklistService>();
builder.Services.AddScoped<IGeneratePdfService, GeneratePdfService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<TokenUtil>();


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
