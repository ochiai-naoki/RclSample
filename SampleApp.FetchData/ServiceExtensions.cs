using Microsoft.Extensions.DependencyInjection;
using SampleApp.FetchData.Data;

namespace SampleApp.FetchData;

public static class ServiceExtensions
{
    public static IServiceCollection AddFetchData(this IServiceCollection services)
        => services.AddSingleton<WeatherForecastService>();
}