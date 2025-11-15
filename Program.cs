using ir_coding_task_csv_validator.Helpers;
using ir_coding_task_csv_validator.Services;
using ir_coding_task_csv_validator.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ICsvMapper, CsvMapper>();
builder.Services.AddScoped<IJobTitleMapper, JobTitleMapper>();
builder.Services.AddScoped<IUserValidator, UserValidator>();
builder.Services.AddScoped<IValidatorService, ValidatorService>();
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
