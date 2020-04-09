using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;

namespace Application
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
			ConfigureService.ConfigureDependenciesService(services);
			ConfigureRepository.ConfigureDependenciesRepository(services);

			var signingConfigurations = new SigningConfigurations();
			services.AddSingleton(signingConfigurations);

			var tokenConfigurations = new TokenConfigurations();
			new ConfigureFromConfigurationOptions<TokenConfigurations>(
				Configuration.GetSection("TokenConfigurations"))
				.Configure(tokenConfigurations);
			services.AddSingleton(tokenConfigurations);

			services.AddAuthentication(authOptions =>
			{
				authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(bearerOptions =>
			{
				var paramsValidation = bearerOptions.TokenValidationParameters;
				paramsValidation.IssuerSigningKey = signingConfigurations.Key;
				paramsValidation.ValidAudience = tokenConfigurations.Audience;
				paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
				paramsValidation.ValidateIssuerSigningKey = true;
				paramsValidation.ValidateLifetime = true;
				paramsValidation.ClockSkew = TimeSpan.Zero;
			});

			services.AddAuthorization(auth =>
			{
				auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
					.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
					.RequireAuthenticatedUser().Build());
			});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(
				"v1",
				new OpenApiInfo
				{
					Title = "Curso de AspNetCore 3.1",
					Version = "v1",
					Description = "Exemplo de API REST criada com ASP.NET Core",
					Contact = new OpenApiContact
					{
						Name = "Esteban Perea",
						Url = new Uri("https://github.com/EstebanAdao")
					}
				});
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Entre com o token JWT",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new List<string>()
					}
				});
			});

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.RoutePrefix = string.Empty;
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
			});

			var option = new RewriteOptions();
			option.AddRedirect("^$", "api-docs");
			app.UseRewriter(option);

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
