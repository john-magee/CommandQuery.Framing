using CommandQuery.Framing;
using Newtonsoft.Json;

namespace CommandQueryApiSample;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddCommandQuery(typeof(Startup).Assembly);
        
        services
            .AddControllers()   
            .AddNewtonsoftJson(x => new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}