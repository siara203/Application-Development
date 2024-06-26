using TrainingFPTCo.DataDBContext;
using Microsoft.EntityFrameworkCore;

namespace TrainingFPTCo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddSession(options =>
			{
				options.Cookie.Name = ".AdventureWorks.Session";
				options.IdleTimeout = TimeSpan.FromMinutes(30);
				// Thiết lập cookie có thời gian sống lâu hơn so với session để session không bị xóa khi trình duyệt đóng
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


			// connect database
			var provider = builder.Services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            builder.Services.AddDbContext<TrainingDbContext>(item => item.UseSqlServer(configuration.GetConnectionString("myconn")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
