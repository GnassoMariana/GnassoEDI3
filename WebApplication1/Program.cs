

using GnassoEDI3.DataAccess;
using GnassoEDI3.Repository.IRepositories;
using GnassoEDI3.Repository.Repositories;
using GnassoEDI3.Services.Interfaces;
using GnassoEDI3.Services.Services;
using GnassoEDI3.Web.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(EmpleadoMappingProfile));


builder.Services.AddDbContext<DbDataAccess>(options =>
{
    
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.MigrationsAssembly("GnassoEDI3.Web")
    );

    
    options.UseLazyLoadingProxies();
});


builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRegistroAsistenciaRepository, RegistroAsistenciaRepository>();
builder.Services.AddScoped<IReporteMensualRepository, ReporteMensualRepository>();

builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRegistroAsistenciaService, RegistroAsistenciaService>();
builder.Services.AddScoped<IReporteMensualService, ReporteMensualService>();

builder.Services.AddAutoMapper(typeof(EmpleadoMappingProfile));
builder.Services.AddAutoMapper(typeof(RegistroAsistenciaMappingProfile));
builder.Services.AddAutoMapper(typeof(ReporteMensualMappingProfile));
builder.Services.AddAutoMapper(typeof(UsuarioMappingProfile));




var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

