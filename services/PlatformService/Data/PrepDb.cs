using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PlatformService.Data;

public static class PrepDb
{
    public static void PrepPopulation(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        AppDbContext? context = serviceScope.ServiceProvider.GetService<AppDbContext>();
        if (context != null)
        {
            SeedData(context);
        }
    }

    private static void SeedData(AppDbContext context)
    {
        if (!context.Platforms.Any())
        {
            Console.WriteLine(("---> seeding data..."));

            context.Platforms.AddRange(
                new Models.Platform() {Name = "DotNet", Publisher = "M$", Cost="Bajillions"}
            );

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("---> we already have data");
        }

    }
}