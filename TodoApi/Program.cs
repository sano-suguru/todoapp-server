using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TodoApi.Data;

namespace TodoApi {
  public class Program {
    public static void Main(string[] args) {
      var webhost = CreateWebHostBuilder(args).Build();
      using (var serviceScope = webhost.Services.CreateScope()) {
        var serviceProvider = serviceScope.ServiceProvider;
        var env = serviceProvider.GetRequiredService<IHostingEnvironment>();
        if (env.IsDevelopment()) {
          try {
            var context = serviceProvider.GetRequiredService<MockDbContext>();
            context.Database.EnsureCreated();
          } catch (Exception ex) {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "データベース初期化中にエラーが発生しました。");
          }
        }
      }
      webhost.Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
  }
}
