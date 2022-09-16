using AspCoreApiDapperProject.Models;
using AspCoreApiDapperProject.Service;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AspCoreApiDapperProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddXmlDataContractSerializerFormatters();

            services.AddScoped<IEmployeeService, EmployeeRepository>();
            services.AddScoped<IEmployeeRepository, CrudEmployeeRepository>();
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.DefaultApiVersion = new ApiVersion(2, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;

                config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
            });
            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Details", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [Sapce] and then your valid token in  the text input below.\r\n\r\nExample: \"Bearer    akjskajskskdjdsjj \""
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                new OpenApiSecurityScheme
                {
                    Reference=new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{ }

                 }
                });
            });


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = Configuration["Jwt:ValidIssuer"],
                        ValidAudience = Configuration["Jwt:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]))
                    };
                });
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));

            services.AddCors(option => option.AddPolicy("APIPolicy", builder => {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            }));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspCoreApiDapperProject v1"));
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = (c) =>
                    {
                        var exception = c.Features.Get<IExceptionHandlerFeature>();
                        var statuscode = exception.Error.GetType().Name switch
                        {
                            "ArgumentException" => HttpStatusCode.BadRequest,
                            _ => HttpStatusCode.ServiceUnavailable
                        };
                        c.Response.StatusCode = (int)statuscode;
                        var content = Encoding.UTF8.GetBytes($"Error[{exception.Error.Message}]");
                        c.Response.Body.WriteAsync(content, 0, content.Length);

                        return Task.CompletedTask;
                    }
                });
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            //app.UseMvc();
            //app.UseMiddleware<HeaderMiddleware>();

            app.UseCors("APIPolicy");
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
