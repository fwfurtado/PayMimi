using FluentValidation;
using PayMimi.Domain.Services;
using PayMimi.Infra.Http.Clients;
using PayMimi.Web.Requests;
using PayMimi.Web.Validations;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddScoped<IValidator<CustomerRegistrationIntentRequest>, CustomerRegistrationIntentValidator>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddTransient<AddApiKeyHandler>();


builder.Services.AddRefitClient<ISocialNumberClient>()
    .ConfigureHttpClient(c => { c.BaseAddress = new Uri("http://localhost:3000/api"); })
    .AddHttpMessageHandler<AddApiKeyHandler>();

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