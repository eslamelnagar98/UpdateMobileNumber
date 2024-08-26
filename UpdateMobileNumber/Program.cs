var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, configuration) =>
    {
        configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddIOptions()
                .AddLeasingIhcRefitClientInternal(serviceProvider => serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value.ApiBaseUrl)
                .AddUpdateMobileNumberServices();
    })
    .UseNLog()
    .Build();

var service = host.Services.GetRequiredService<IMobileNumberUpdateService>();
await service.ProcessAndFetchIdsAsync();
