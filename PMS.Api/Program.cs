using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using PMS.Application.Interfaces;
using PMS.Application.Repository_Interfaces;
using PMS.Application.Services;
using PMS.Domain;
using PMS.Infra;
using System.Text;

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
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientService,PatientService>();
builder.Services.AddScoped<IPatientRepository,PatientRepository>();
builder.Services.AddScoped<IDeviceService,DeviceService>();
builder.Services.AddScoped<IDeviceRepository,DeviceRepository>();
builder.Services.AddScoped<IVitalSignService,VitalSignService>();
builder.Services.AddScoped<IVitalSignRepository,VitalSignRepository>();
<<<<<<< HEAD
builder.Services.AddScoped<IMedicalhistoryService,MedicalHistoryService>();
builder.Services.AddScoped<IMedicalHistoryRepository,MedicalHistoryRepository>();
=======
builder.Services.AddScoped<IHospitalRepository,HospitalRepository>();
builder.Services.AddScoped<IHospitalService,HospitalService>(); 
builder.Services.AddScoped<IDoctorRepository,DoctorRepository>();   
builder.Services.AddScoped<IDoctorService,DoctorService>();
>>>>>>> HospitalService
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000") // Allow React app origin
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use CORS policy
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
