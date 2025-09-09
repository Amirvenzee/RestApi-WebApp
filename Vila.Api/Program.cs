
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using Vila.WebApi.Utility;
using VILA.Api.Context;
using VILA.Api.Services.Customer;
using VILA.Api.Services.Detail;
using VILA.Api.Services.Vila;
using VILA.Api.Utility;

var builder = WebApplication.CreateBuilder(args);


var services = builder.Services;
services.AddCors();
services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

services.AddDbContext<DataContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("Man"));
});



#region Swagger

services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerVilaDocument>();



builder.Services.AddSwaggerGen();
#endregion

#region Dependency
services.AddTransient<IVilaService, VilaService>();
services.AddTransient<IDetailService, DetailService>();
services.AddTransient<ICustomerService, CustomerService>();
#endregion

#region Versioning
services.AddApiVersioning(option =>
{
    option.AssumeDefaultVersionWhenUnspecified = true;
    option.DefaultApiVersion = new ApiVersion(1,0);
    option.ReportApiVersions = true;
    option.ApiVersionReader = new UrlSegmentApiVersionReader();
    // option.ApiVersionReader = new QueryStringApiVersionReader("X-ApiVersion");
    //  option.ApiVersionReader = new HeaderApiVersionReader("X-ApiVersion");




}).AddApiExplorer(x =>
{
    x.GroupNameFormat = "'v'VVV";
    //  / Swagger / V1 Or v2 / swagger.json"

    //  x.GroupNameFormat = "'v'VVVV";
    // / Swagger / V1.0 Or v2.0 / swagger.json"
});
#endregion

#region Jwt
var JwtSettingSection = builder.Configuration.GetSection("JWTSettings");
services.Configure<JWTSettings>(JwtSettingSection);

var jwtsetting = JwtSettingSection.Get<JWTSettings>();

var key = Encoding.ASCII.GetBytes(jwtsetting.Secret);


services.AddAuthentication(x =>
{
    x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtsetting.Issuer,
        ValidateIssuer = true,
        ValidAudience = jwtsetting.Audience,
        ValidateAudience = true,
        ValidateLifetime = true
    };
});
#endregion
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        var provider = app.DescribeApiVersions();
        //     var provider = app.Services.CreateScope().ServiceProvider
        //.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var item in provider)
        {
            x.SwaggerEndpoint($"/Swagger/{item.GroupName}/swagger.json", item.GroupName.ToString());
        }

        //x.SwaggerEndpoint("/Swagger/VilaOpenApi/swagger.json", "Vila Open Api");
        x.RoutePrefix = "";
    });
}

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
