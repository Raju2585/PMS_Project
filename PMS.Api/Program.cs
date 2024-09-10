using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Application.Services;
using PMS.Infra;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<IApplicationDbContext,ApplicationDbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDb"),
b=>b.MigrationsAssembly("PMS.Api")
));

builder.Services.AddScoped<IPatientService,PatientService>();
builder.Services.AddScoped<IPatientRepository,PatientRepository>();
builder.Services.AddScoped<IDeviceService,DeviceService>();
builder.Services.AddScoped<IDeviceRepository,DeviceRepository>();
builder.Services.AddScoped<IVitalSignService,VitalSignService>();
builder.Services.AddScoped<IVitalSignRepository,VitalSignRepository>();
builder.Services.AddScoped<IMedicalHistoryRepository,MedicalHistoryRepository>();
builder.Services.AddScoped<IMedicalhistoryService, MedicalHistoryService>();


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
