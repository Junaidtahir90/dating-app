using System.Text;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API
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
            //var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            // To Inject Dbcontect and load ConnectionString 
              services.AddDbContext<DataContext>(options =>options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
              services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #region Enbale CORS
             // To enable cor add service a below
             services.AddCors(options =>
              {
                    options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
              });
              #endregion
             // Need to learn/R&D Signleton & Transit,AddScoped
              #region  Add Interfaces
              services.AddScoped<IAuthRepositry,AuthRepositry>();  // to add reference of Interface w.r.t Repositry
              #endregion
              
              #region  Enable Authorization
              services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
              AddJwtBearer( options=>{
                  options.TokenValidationParameters = new TokenValidationParameters{
                      ValidateIssuerSigningKey=true,
                      IssuerSigningKey= new SymmetricSecurityKey(Encoding.ASCII.
                      GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                      ValidateIssuer = false,
                      ValidateAudience = false,
                  };
              });  // to add reference of Interface w.r.t Repositry
              #endregion
              
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               // app.UseHsts();
            }

            //app.UseHttpsRedirection();
            // To enable CORS
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
