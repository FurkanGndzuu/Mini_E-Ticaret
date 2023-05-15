

using ETicaretAPI.API.Configurations;
using ETicaretAPI.API.Filters;
using ETicaretAPI.Application;
using ETicaretAPI.Application.Validators;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Infrastructure.Services.Storage.Azure;
using ETicaretAPI.Infrastructure.Services.Storage.Local;
using ETicaretAPI.Presistance;
using ETicaretAPI.SignalR;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRService();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("admin", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true, //Oluþturulacak token deðerini kimlerin/hangi originlerin/sitelerin kullanýcý belirlediðimiz deðerdir. -> www.bilmemne.com
        ValidateIssuer = true, //Oluþturulacak token deðerini kimin daðýttýný ifade edeceðimiz alandýr. -> www.myapi.com
        ValidateLifetime = true, //Oluþturulan token deðerinin süresini kontrol edecek olan doðrulamadýr.
        ValidateIssuerSigningKey = true, //Üretilecek token deðerinin uygulamamýza ait bir deðer olduðunu ifade eden suciry key verisinin doðrulanmasýdýr.

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:securityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
       
        NameClaimType = ClaimTypes.Name
    };
});





// Add services to the container.
//builder.Services.AddCors(options => options.AddPolicy("myClient" , builder => 
//builder.WithOrigins("http://localhost:4200/" , "https://localhost:4200/")
//.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
    
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowCredentials().AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddPresistanceServices();

builder.Services.AddControllers(x =>
{
    x.Filters.Add<ValidationFilter>();
    x.Filters.Add<RolePermisionFilter>();
}).AddFluentValidation(configuration =>
configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStorage<AzureStorage>();

Logger logger = new LoggerConfiguration().WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL") , "logs" ,
    needAutoCreateTable : true , 
    columnOptions : new Dictionary<string , ColumnWriterBase>
    {
        {"messageType" , new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
        {"message_template" , new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
        {"level" ,  new LevelColumnWriter(true , NpgsqlDbType.Integer)},
        {"time-Stamp" ,  new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
        {"exception" , new ExceptionColumnWriter(NpgsqlDbType.Text) },
        {"log-event" , new LogEventSerializedColumnWriter(NpgsqlDbType.Text) },
        {"user_name" , new UserNameColumnWriter() }

    }).Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseHttpLogging();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.AddMapHubs();

app.Run();
