namespace UpdateMobileNumber.Extensions;
public static partial class Extension
{
    public static IServiceCollection AddIOptions(this IServiceCollection services)
    {
        services
            .AddOptionsWithValidateOnStart<AppSettings>()
            .Configure<IConfiguration>(
            (options, configuration) =>
             configuration.GetSection(AppSettings.SectionName)
            .Bind(options))
            .ValidateOnStart();
        return services;
    }

    public static IServiceCollection AddILogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.SetMinimumLevel(LogLevel.Trace);
            loggingBuilder.AddNLog(configuration);
        });

        return services;
    }

    public static IServiceCollection AddUpdateMobileNumberServices(this IServiceCollection services)
    {
        services.AddTransient<IFileProcessingFactory, FileProcessingFactory>()
                .AddTransient<ICsvProcessingService, CsvProcessingService>()
                .AddTransient<IExcelProcessingService, ExcelProcessingService>()
                .AddTransient<IMobileNumberUpdateService, MobileNumberUpdateService>()
                .AddTransient<IMobileNumberDatabaseService, MobileNumberDatabaseService>();
        return services;
    }

    public static IServiceCollection AddLeasingIhcRefitClientInternal(this IServiceCollection services,
                                                                      Func<IServiceProvider, string> baseAddressUrlFunc)
    {
        var refitSetting = GenerateRefitSettings();
        services.AddRefitClient<IPolicyUpdateEndpoint>(refitSetting).ConfigureHttpClient((serviceProvider, client) =>
       {
           var baseAddressUrl = baseAddressUrlFunc(serviceProvider);

           client.BaseAddress = new Uri(baseAddressUrl);
       });

        return services;
    }

    private static RefitSettings GenerateRefitSettings()
    {
        return new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer(new JsonSerializerSettings
            {
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy
                    {
                        ProcessDictionaryKeys = true,
                        OverrideSpecifiedNames = true
                    }
                },
                Formatting = Formatting.Indented,
                FloatParseHandling = FloatParseHandling.Decimal,
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
            })
        };
    }
}
