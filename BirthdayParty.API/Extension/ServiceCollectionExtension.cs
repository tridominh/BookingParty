
using Azure.Storage.Blobs;
using BirthdayParty.DAL;
using BirthdayParty.Services.Interfaces;
using BirthdayParty.Services.LocalImages;
using Microsoft.EntityFrameworkCore;

namespace BirthdayParty.API.Extensions;

public static class LocalServiceCollectionExtension
{
    public static IServiceCollection RegisterLocalServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingPartyContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("BirthdayDb")));

        services.AddSingleton(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));
        services.AddScoped<IPackageImageLocalService, PackageImageLocalService>();
        services.AddScoped<IRoomImageLocalService, RoomImageLocalService>();
        return services;
    }

    public static IServiceCollection RegisterOnlineServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingPartyContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("OnlineDb")));

        services.AddSingleton(x => new BlobServiceClient(configuration.GetConnectionString("AzureBlobStorage")));
        services.AddScoped<IPackageImageLocalService, PackageImageLocalService>();
        services.AddScoped<IRoomImageLocalService, RoomImageLocalService>();
        return services;
    }
}
